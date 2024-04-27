using DevMate.Application.Models.Mailing;

namespace DevMate.Application.Abstractions.Repositories;

public interface IEmailTemplateRepository
{
    IEnumerable<EmailTemplate?> GetEmailTemplates();

    void SetEmailTemplate(EmailTemplate template);

    void AddEmailTemplate(EmailTemplate template);
}