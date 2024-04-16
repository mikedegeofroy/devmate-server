namespace DevMate.Application.Abstractions.Telegram.Models;

public record TelegramPeerModel(long Id, string? Username, string DisplayName, string Photo, long AccessHash);