using LearningMate.Core.ServiceContracts.ListeningTopicQuestionsServiceContract;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicQuestionsService;

public partial class ListeningTopicQuestionsService(ILogger<ListeningTopicQuestionsService> logger)
    : IListeningTopicQuestionsService
{
    private readonly ILogger<ListeningTopicQuestionsService> _logger = logger;
}
