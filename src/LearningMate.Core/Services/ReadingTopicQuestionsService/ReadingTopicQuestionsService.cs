using LearningMate.Core.ServiceContracts.ReadingTopicQuestionsServiceContract;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicQuestionsService;

public partial class ReadingTopicQuestionsService(ILogger<ReadingTopicQuestionsService> logger) : IReadingTopicQuestionsService
{
    private readonly ILogger<ReadingTopicQuestionsService> _logger = logger;
}
