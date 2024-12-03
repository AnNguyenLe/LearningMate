using LearningMate.Core.Mappers.SpeakingTopicMappers;
using LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.SpeakingTopicsService;

public partial class SpeakingTopicsService(
    ILogger<SpeakingTopicsService> logger,
    ISpeakingTopicsRepository speakingTopicsRepository,
    SpeakingTopicMapper speakingTopicMapper,
    IExamsRepository examsRepository
) : ISpeakingTopicsService
{
    private readonly ILogger<SpeakingTopicsService> _logger = logger;
    private readonly ISpeakingTopicsRepository _speakingTopicsRepository = speakingTopicsRepository;
    private readonly SpeakingTopicMapper _speakingTopicMapper = speakingTopicMapper;
    private readonly IExamsRepository _examsRepository = examsRepository;
}
