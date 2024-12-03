using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Speaking;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace LearningMate.Infrastructure.Repositories;

public class SpeakingTopicsRepository(
    ILogger<SpeakingTopicsRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : ISpeakingTopicsRepository
{
    private readonly ILogger<SpeakingTopicsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task<Result<int>> AddTopicAsync(SpeakingTopic topic)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO speaking_topics
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
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new speaking topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("add new speaking topic"));
        }
        return totalAffectedRows;
    }

    public async Task<Result<bool>> CheckSpeakingTopicExistsAsync(Guid topicId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = "SELECT COUNT(DISTINCT 1) from speaking_topics WHERE id=@topicId";
        var isExist = dbConnection.ExecuteScalar<bool>(sqlQuery, new { topicId });
        if (isExist == false)
        {
            _logger.LogWarning(
                CommonLoggingMessages.RecordNotFoundWithId,
                nameof(SpeakingTopic),
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(SpeakingTopic), topicId)
            );
        }
        return isExist;
    }
}
