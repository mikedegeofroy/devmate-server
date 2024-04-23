using DevMate.Application.Abstractions.Services;
using DevMate.Application.Models.Mailing;

namespace DevMate.Infrastructure.Integration.Mailgun.Services;

public class MockMailService : IMailService
{
    public void SendMail(string email, MailContent content)
    {
        Console.WriteLine(email);
        Console.WriteLine(content.Content);
    }
}