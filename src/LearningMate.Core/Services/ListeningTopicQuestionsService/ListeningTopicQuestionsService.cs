using LearningMate.Core.Mappers.ListeningTopicQuestionMappers;
using LearningMate.Core.ServiceContracts.ListeningTopicQuestionsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicQuestionsService;

public partial class ListeningTopicQuestionsService(
    ILogger<ListeningTopicQuestionsService> logger,
    ListeningTopicQuestionMapper listeningTopicQuestionMapper,
    IListeningTopicQuestionsRepository listeningTopicQuestionsRepository,
    IListeningTopicsRepository listeningTopicsRepository
) : IListeningTopicQuestionsService
{
    private readonly ILogger<ListeningTopicQuestionsService> _logger = logger;
    private readonly ListeningTopicQuestionMapper _listeningTopicQuestionMapper =
        listeningTopicQuestionMapper;
    private readonly IListeningTopicQuestionsRepository _listeningTopicQuestionsRepository =
        listeningTopicQuestionsRepository;
    private readonly IListeningTopicsRepository _listeningTopicsRepository = listeningTopicsRepository;
}
