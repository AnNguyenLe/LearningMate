using LearningMate.Core.Mappers.ReadingTopicQuestionMappers;
using LearningMate.Core.ServiceContracts.ReadingTopicQuestionsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicQuestionsService;

public partial class ReadingTopicQuestionsService(
    ILogger<ReadingTopicQuestionsService> logger,
    ReadingTopicQuestionMapper readingTopicQuestionMapper,
    IReadingTopicQuestionsRepository readingTopicQuestionsRepository,
    IReadingTopicsRepository readingTopicsRepository
) : IReadingTopicQuestionsService
{
    private readonly ILogger<ReadingTopicQuestionsService> _logger = logger;
    private readonly ReadingTopicQuestionMapper _readingTopicQuestionMapper =
        readingTopicQuestionMapper;
    private readonly IReadingTopicQuestionsRepository _readingTopicQuestionsRepository =
        readingTopicQuestionsRepository;
    private readonly IReadingTopicsRepository _readingTopicsRepository = readingTopicsRepository;
}
