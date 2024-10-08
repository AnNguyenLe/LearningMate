namespace LearningMate.Core.ServiceContracts.HtmlTemplate;

public interface IEmailHtmlTemplateService
{
    string GenerateRegisterEmailConfirmationTemplate(
        Guid userId,
        string username,
        string emailToken
    );
    string GeneratePasswordResetEmailConfirmationTemplate(
        Guid userId,
        string username,
        string emailToken
    );
}
