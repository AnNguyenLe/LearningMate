using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Listening;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace LearningMate.Infrastructure.Repositories;

public class ListeningTopicsRepository(
    ILogger<ListeningTopicsRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : IListeningTopicsRepository
{
    private readonly ILogger<ListeningTopicsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<int>> AddTopicAsync(ListeningTopic topic)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO listening_topics
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
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new listening topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("add new listening topic"));
        }

        return totalAffectedRows;
    }

    public async Task<Result<bool>> CheckListeningTopicExistsAsync(Guid topicId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = "SELECT COUNT(DISTINCT 1) from listening_topics WHERE id=@topicId";
        var isExist = dbConnection.ExecuteScalar<bool>(sqlQuery, new { topicId });

        if (isExist == false)
        {
            _logger.LogWarning(
                CommonLoggingMessages.RecordNotFoundWithId,
                nameof(ListeningTopic),
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ListeningTopic), topicId)
            );
        }

        return isExist;
    }
}
