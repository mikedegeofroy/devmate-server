using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts.Mailing;
using DevMate.Application.Models.Mailing;
using Fluid;

namespace DevMate.Application.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly FluidParser _fluidParser;

    public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, FluidParser fluidParser)
    {
        _emailTemplateRepository = emailTemplateRepository;
        _fluidParser = fluidParser;
    }

    public IEnumerable<EmailTemplate?> GetTemplates()
    {
        return _emailTemplateRepository.GetEmailTemplates();
    }

    public EmailTemplate? GetTemplate(string id)
    {
        return _emailTemplateRepository.GetEmailTemplates().FirstOrDefault(x => x?.Id == id);
    }

    public void PostTemplate(EmailTemplate template)
    {
        _emailTemplateRepository.AddEmailTemplate(template);
    }

    public MailContent RenderTemplate(string templateId, object variables)
    {
        EmailTemplate? source = GetTemplate(templateId);

        if (!_fluidParser.TryParse(source?.Template ?? "",
                out IFluidTemplate? template,
                out string? error
            )) throw new Exception(error);

        var context = new TemplateContext(variables);

        string? content = template.RenderAsync(context).GetAwaiter().GetResult();

        return new MailContent(source.Subject, content);
    }
}