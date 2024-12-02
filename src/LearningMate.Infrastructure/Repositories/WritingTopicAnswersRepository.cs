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

public class WritingTopicAnswersRepository(
    ILogger<WritingTopicAnswersRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : IWritingTopicAnswersRepository
{
    private readonly ILogger<WritingTopicAnswersRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<int>> AddAnswerAsync(WritingTopicAnswer answer)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO writing_topic_answers
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
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new writing topic answer");
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("add new writing topic answer")
            );
        }

        return totalAffectedRows;
    }
}
