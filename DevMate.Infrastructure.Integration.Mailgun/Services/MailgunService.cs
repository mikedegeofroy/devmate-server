using DevMate.Application.Abstractions.Services;
using DevMate.Application.Models.Mailing;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace DevMate.Infrastructure.Integration.Mailgun.Services;

public class MailgunService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailgunService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMail(string email, MailContent content)
    {
        var options = new RestClientOptions()
        {
            BaseUrl = new Uri("https://api.mailgun.net/v3"),
            Authenticator =
                new HttpBasicAuthenticator("api", _configuration.GetSection("AppSettings:MailgunApiKey").Value!)
        };

        var client = new RestClient(options);

        var request = new RestRequest();
        request.AddParameter("from", "noreply@devmate.me");
        request.AddParameter("to", email);
        request.AddParameter("subject", content.Subject);
        request.AddParameter("html", content.Content);
        request.Method = Method.Post;
        request.Resource = "devmate.me/messages";

        client.Execute(request);
    }
}