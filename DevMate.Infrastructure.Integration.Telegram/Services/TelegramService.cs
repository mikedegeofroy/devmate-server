using ParkingApp.Application.Abstractions.Telegram.Models;
using ParkingApp.Application.Abstractions.Telegram.Services;
using ParkingApp.Application.Models;
using TL;
using WTelegram;

namespace DevMate.Infrastructure.Integration.Telegram.Services;

public class TelegramService : ITelegramService, IDisposable
{
    private readonly WTelegram.Client _client;
    private Messages_Chats? _chats;

    public TelegramService()
    {
        _chats = null;
        _client = new Client(24109502, "fb1de0235f99255acbeae97741af6076");
        DoLogin("+79213877660").GetAwaiter().GetResult();
    }

    private async Task DoLogin(string? loginInfo) // (add this method to your code)
    {
        while (_client.User == null)
            switch (await _client.Login(loginInfo)) // returns which config is needed to continue login
            {
                case "verification_code":
                    Console.Write("Code: ");
                    loginInfo = Console.ReadLine();
                    break;
                case "password":
                    loginInfo = "SaintPetersburg2020";
                    break;
                case "server_address":
                    loginInfo = "149.154.167.40:443";
                    break;
                default:
                    loginInfo = null;
                    break;
            }

        Console.WriteLine($"We are logged-in as {_client.User} (id {_client.User.id})");
    }

    public async Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit)
    {
        if (_chats == null) await GetPeersAsync();

        ChatBase? peer = _chats?.chats[id];
        if (peer == null) throw new IndexOutOfRangeException();

        var res = new List<TelegramMessageModel>();

        for (int offsetId = 0;;)
        {
            Messages_MessagesBase? messages = await _client.Messages_GetHistory(peer, offsetId);
            if (messages.Messages.Length == 0) break;
            if (messages.Messages.Last().Date < limit) break;
            foreach (MessageBase? msgBase in messages.Messages)
            {
                IPeerInfo?
                    from = messages.UserOrChat(msgBase.From ??
                                               msgBase.Peer); // from can be TelegramUserModel/Chat/Channel
                string path = "./wwwroot/profile_pictures/" + from.ID.ToString() + ".png";

                if (!File.Exists(path))
                {
                    FileStream file = File.Create(path);
                    await _client.DownloadProfilePhotoAsync(from, file);
                }

                if (from is not User user) continue;
                switch (msgBase)
                {
                    case Message msg:
                        res.Add(new TelegramMessageModel(
                            new TelegramPeerModel(from.ID, from.ToString(), from.MainUsername, from.ID.ToString(),
                                user.access_hash),
                            msg.message,
                            msg.Date));
                        break;
                    case MessageService ms:
                        Console.WriteLine($"{from} [{ms.action.GetType().Name[13..]}]");
                        break;
                }
            }

            offsetId = messages.Messages[^1].ID;
        }

        return res;
    }

    public async Task<IEnumerable<TelegramPeerModel>> GetPeersAsync()
    {
        _chats = await _client.Messages_GetAllChats();

        if (_chats == null) return Enumerable.Empty<TelegramPeerModel>();

        var parsedChats = _chats.chats.Where(c => c.Value.IsActive)
            .Select(chat => new TelegramPeerModel(chat.Key, chat.Value.Title, chat.Value.MainUsername,
                chat.Value.Photo?.ToString() ?? "N/A", 0))
            .ToList();

        return parsedChats;
    }

    public async void Send(TelegramUserModel user, string message)
    {
        await _client.SendMessageAsync(new InputPeerUser(user.Id, user.AccessHash), message);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}