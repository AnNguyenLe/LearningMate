using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Writing;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace LearningMate.Infrastructure.Repositories;

public class WritingTopicsRepository(
    ILogger<WritingTopicsRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : IWritingTopicsRepository
{
    private readonly ILogger<WritingTopicsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<int>> AddTopicAsync(WritingTopic topic)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO writing_topics
                    (
                        id,
                        content,
                        serialized_resources_url,
                        category,
                        score_band,
                        score,
                        exam_id
                    )
                VALUES
                    (
                        @Id,
                        @Content,
                        @SerializedResourcesUrl,
                        @Category,
                        @ScoreBand,
                        @Score,
                        @ExamId
                    );
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, topic);

        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new writing topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("add new writing topic"));
        }

        return totalAffectedRows;
    }

    public async Task<Result<bool>> CheckReadingTopicExistsAsync(Guid topicId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = "SELECT COUNT(DISTINCT 1) from writing_topics WHERE id=@topicId";
        var isExist = dbConnection.ExecuteScalar<bool>(sqlQuery, new { topicId });

        if (isExist == false)
        {
            _logger.LogWarning(
                CommonLoggingMessages.RecordNotFoundWithId,
                nameof(WritingTopic),
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(WritingTopic), topicId)
            );
        }

        return isExist;
    }
}
