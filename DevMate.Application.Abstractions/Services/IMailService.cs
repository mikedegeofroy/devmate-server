using DevMate.Application.Models.Mailing;

namespace DevMate.Application.Abstractions.Services;

public interface IMailService
{
    void SendMail(string email, MailContent content);
}