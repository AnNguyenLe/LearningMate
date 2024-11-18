using LearningMate.Core.Mappers.ListeningTopicMappers;
using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicsService;

public partial class ListeningTopicsService(
    ILogger<ListeningTopicsService> logger,
    IListeningTopicsRepository listeningTopicsRepository,
    ListeningTopicMapper listeningTopicMapper,
    IExamsRepository examsRepository
) : IListeningTopicsService
{
    private readonly ILogger<ListeningTopicsService> _logger = logger;
    private readonly IListeningTopicsRepository _listeningTopicsRepository = listeningTopicsRepository;
    private readonly ListeningTopicMapper _listeningTopicMapper = listeningTopicMapper;
    private readonly IExamsRepository _examsRepository = examsRepository;
}
