namespace ParkingApp.Application.Models;

public record TelegramUserModel(long Id, string Username, string DisplayName, string Photo, long AccessHash);