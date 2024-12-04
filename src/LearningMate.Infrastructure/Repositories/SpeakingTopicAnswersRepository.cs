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

public class SpeakingTopicAnswersRepository(
    ILogger<SpeakingTopicAnswersRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : ISpeakingTopicAnswersRepository
{
    private readonly ILogger<SpeakingTopicAnswersRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    public async Task<Result<int>> AddAnswerAsync(SpeakingTopicAnswer answer)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO speaking_topic_answers
                    (
                        id,
                        content,
                        topic_id
                    )
                VALUES
                    (
                        @Id,
                        @Content,
                        @TopicId
                    );
            """;
        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, answer);
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new speaking topic answer");
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("add new speaking topic answer")
            );
        }
        return totalAffectedRows;
    }
}
