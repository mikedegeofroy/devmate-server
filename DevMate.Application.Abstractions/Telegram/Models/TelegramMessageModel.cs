namespace DevMate.Application.Abstractions.Telegram.Models;

public record TelegramMessageModel(TelegramPeerModel Peer, string? Message, DateTime DateTime);