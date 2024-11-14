using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace LearningMate.Infrastructure.Repositories;

public class ReadingTopicsRepository(
    ILogger<ReadingTopicsRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : IReadingTopicsRepository
{
    private readonly ILogger<ReadingTopicsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<int>> AddTopicAsync(ReadingTopic topic)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO reading_topics
                    (
                        id,
                        category,
                        title,
                        content,
                        score_band,
                        score,
                        exam_id
                    )
                VALUES
                    (
                        @Id,
                        @Category,
                        @Title,
                        @Content,
                        @ScoreBand,
                        @Score,
                        @ExamId
                    );
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, topic);

        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("add new reading topic"));
        }

        return totalAffectedRows;
    }

    public async Task<Result<bool>> CheckReadingTopicExistsAsync(Guid topicId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = "SELECT COUNT(DISTINCT 1) from reading_topics WHERE id=@topicId";
        var isExist = dbConnection.ExecuteScalar<bool>(sqlQuery, new { topicId });

        if (isExist == false)
        {
            _logger.LogWarning(
                CommonLoggingMessages.RecordNotFoundWithId,
                nameof(ReadingTopic),
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ReadingTopic), topicId)
            );
        }

        return isExist;
    }
}
