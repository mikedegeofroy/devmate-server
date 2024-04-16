using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Infrastructure.Integration.Telegram.User.Services;
using NSubstitute;
using NSubstitute.Exceptions;
using Xunit;

namespace DevMate.Infrastructure.Integration.Telegram.Tests.Services;

public class TelegramUserClientTests
{
    [Fact]
    public async Task Level2Filter_Success_WhenRecipientDidNotReceiveLevel1Message()
    {
        // Arrange
        IFileSystem mockFs = Substitute.For<IFileSystem>();
        // var service = new TelegramUserClientDeprecated(mockFs, "+79213877660", new MemoryStream());

        // ITelegramUserClient userClient = await service.Build();

        // Act
        // service.GetPeersAsync();

        // Assert
    }
}