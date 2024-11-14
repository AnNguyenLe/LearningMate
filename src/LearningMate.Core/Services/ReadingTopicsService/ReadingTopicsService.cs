using LearningMate.Core.Mappers.ReadingTopicMappers;
using LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicsService;

public partial class ReadingTopicsService(
    ILogger<ReadingTopicsService> logger,
    IReadingTopicsRepository readingTopicsRepository,
    ReadingTopicMapper readingTopicMapper,
    IExamsRepository examsRepository
) : IReadingTopicsService
{
    private readonly ILogger<ReadingTopicsService> _logger = logger;
    private readonly IReadingTopicsRepository _readingTopicsRepository = readingTopicsRepository;
    private readonly ReadingTopicMapper _readingTopicMapper = readingTopicMapper;
    private readonly IExamsRepository _examsRepository = examsRepository;
}
