namespace DevMate.Application.Models.Analytics;

public record TelegramUserModel(long Id, string Username, string DisplayName, string Photo, long AccessHash);