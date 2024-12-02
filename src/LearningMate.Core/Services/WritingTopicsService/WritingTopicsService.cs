using LearningMate.Core.Mappers.WritingTopicMappers;
using LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.WritingTopicsService;

public partial class WritingTopicsService(
    ILogger<WritingTopicsService> logger,
    IWritingTopicsRepository writingTopicsRepository,
    WritingTopicMapper writingTopicMapper,
    IExamsRepository examsRepository
) : IWritingTopicsService
{
    private readonly ILogger<WritingTopicsService> _logger = logger;
    private readonly IWritingTopicsRepository _writingTopicsRepository = writingTopicsRepository;
    private readonly WritingTopicMapper _writingTopicMapper = writingTopicMapper;
    private readonly IExamsRepository _examsRepository = examsRepository;
}
