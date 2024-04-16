using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Telegram.Models;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;
using TL;
using WTelegram;

namespace DevMate.Infrastructure.Integration.Telegram.User.Services;

public class TelegramUserClient : ITelegramUserClient
{
    private Messages_Chats? _chats;
    private readonly IFileSystem _fileSystem;
    private readonly Client _client;

    public TelegramUserClient(IFileSystem fileSystem, Client client)
    {
        _fileSystem = fileSystem;
        _client = client;
    }

    public async Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit,
        UserDto user, Stream store)
    {
        if (_chats == null) await GetPeersAsync(user, store);

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
                                               msgBase.Peer);

                if (from is not TL.User userPeer) continue;
                switch (msgBase)
                {
                    case Message msg:
                        res.Add(new TelegramMessageModel(
                            new TelegramPeerModel(from.ID, from.ToString(), from.MainUsername,
                                DownloadProfilePicture(from),
                                userPeer.access_hash),
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
        
        store.DisposeAsync().GetAwaiter().GetResult();
        return res;
    }

    public async Task<IEnumerable<TelegramPeerModel>> GetPeersAsync(UserDto user, Stream store)
    {

        _chats = await _client.Messages_GetAllChats();

        if (_chats == null) return Enumerable.Empty<TelegramPeerModel>();

        var parsedChats = _chats.chats.Where(c => c.Value.IsActive)
            .Select(chat =>
                new TelegramPeerModel(
                    chat.Key,
                    chat.Value.Title,
                    chat.Value.MainUsername,
                    DownloadProfilePicture(chat.Value),
                    0))
            .ToList();

        _client.Dispose();
        store.DisposeAsync().GetAwaiter().GetResult();
        return parsedChats;
    }

    public async void Send(TelegramUserModel recipient, string message, UserDto user, Stream store)
    {
        await _client.SendMessageAsync(new InputPeerUser(recipient.Id, recipient.AccessHash), message);
        store.DisposeAsync().GetAwaiter().GetResult();
    }

    public TelegramUserModel GetUser(string phone, Stream store)
    {
        return new TelegramUserModel(_client.User.ID, _client.User.username, _client.User.MainUsername,
            DownloadProfilePicture(_client.User), _client.User.access_hash);
    }

    private string DownloadProfilePicture(IPeerInfo peerInfo)
    {
        string relativePath = "/profile_pictures/" + Guid.NewGuid() + ".jpg";

        if (_fileSystem.Exists(relativePath)) return relativePath;

        Task.Run(async () =>
        {
            var file = new MemoryStream();
            await _client.DownloadProfilePhotoAsync(peerInfo, file);
            await _fileSystem.WriteFile(file, relativePath);
        });

        return relativePath;
    }
}