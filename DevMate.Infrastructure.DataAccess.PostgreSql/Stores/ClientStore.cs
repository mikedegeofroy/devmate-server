using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Stores;

public class ClientStore : Stream
{
    private readonly NpgsqlConnection _sql;
    private readonly string _sessionName;
    private byte[]? _data;
    private int _dataLen;
    private DateTime _lastWrite;
    private Task? _delayedWrite;

    public ClientStore(IPostgresConnectionProvider connection, string sessionName)
    {
        _sessionName = sessionName;

        string connectionString = connection.GetConnectionAsync(default).GetAwaiter().GetResult().ConnectionString;

        _sql = new NpgsqlConnection(connectionString +
                                    ";Password=postgres;Keepalive=60;Pooling=true;ConnectionIdleLifetime=60;");
        _sql.Open();

        using (var create =
               new NpgsqlCommand(
                   "CREATE TABLE IF NOT EXISTS TelegramSessions (name text NOT NULL PRIMARY KEY, data bytea)", _sql))
            create.ExecuteNonQuery();

        using var cmd = new NpgsqlCommand($"SELECT data FROM TelegramSessions WHERE name = '{_sessionName}'", _sql);

        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        if (rdr.Read())
            _dataLen = ((_data = rdr[0] as byte[])!).Length;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        Array.Copy(_data!, 0, buffer, offset, count);
        return count;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        _data = buffer;
        _dataLen = count;

        if (_delayedWrite != null) return;
        int left = 1000 - (int)(DateTime.UtcNow - _lastWrite).TotalMilliseconds;
        if (left < 0)
        {
            using var cmd =
                new NpgsqlCommand(
                    $"INSERT INTO TelegramSessions (name, data) VALUES ('{_sessionName}', @data) ON CONFLICT (name) DO UPDATE SET data = EXCLUDED.data",
                    _sql);
            cmd.Parameters.AddWithValue("data", count == buffer.Length ? buffer : buffer[offset..(offset + count)]);
            cmd.ExecuteNonQuery();
            _lastWrite = DateTime.UtcNow;
        }
        else // delay writings for a full second
            _delayedWrite = Task.Delay(left).ContinueWith(_ =>
            {
                lock (this)
                {
                    _delayedWrite = null;
                    Write(_data, 0, _dataLen);
                }
            });
    }

    public override ValueTask DisposeAsync()
    {
        _sql.Close();
        _sql.Dispose();
        return base.DisposeAsync();
    }

    public override long Length => _dataLen;
    public override long Position { get => 0; set { } }
    public override bool CanSeek => false;
    public override bool CanRead => true;
    public override bool CanWrite => true;
    public override long Seek(long offset, SeekOrigin origin) => 0;
    public override void SetLength(long value) { }
    public override void Flush() { }
}