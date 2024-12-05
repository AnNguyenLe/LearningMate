using FluentResults;

namespace LearningMate.AI.ServiceContracts.PromptServiceContract;

public interface IPromptService
{
    Task<Result<string>> GenerateContent(string prompt);
}
