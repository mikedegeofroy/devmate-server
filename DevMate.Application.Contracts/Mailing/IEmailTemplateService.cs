using DevMate.Application.Models.Mailing;

namespace DevMate.Application.Contracts.Mailing;

public interface IEmailTemplateService
{
    IEnumerable<EmailTemplate?> GetTemplates();

    EmailTemplate? GetTemplate(string id);

    void PostTemplate(EmailTemplate template);
    
    MailContent RenderTemplate(string templateId, object variables);
}