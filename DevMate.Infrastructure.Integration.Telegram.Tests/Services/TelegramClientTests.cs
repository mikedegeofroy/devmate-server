using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Infrastructure.Integration.Telegram.Services;
using NSubstitute;
using NSubstitute.Exceptions;
using Xunit;

namespace DevMate.Infrastructure.Integration.Telegram.Tests.Services;

public class TelegramClientTests
{
    [Fact]
    public async Task Level2Filter_Success_WhenRecipientDidNotReceiveLevel1Message()
    {
        // Arrange
        IFileSystem mockFs = Substitute.For<IFileSystem>();
        var service = new TelegramClientService(mockFs, "+79213877660", new MemoryStream());

        ITelegramClient client = await service.Build();

        // Act
        // service.GetPeersAsync();

        // Assert
    }
}