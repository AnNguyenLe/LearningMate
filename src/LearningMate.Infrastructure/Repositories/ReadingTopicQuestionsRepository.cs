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

public class ReadingTopicQuestionsRepository(
    ILogger<ReadingTopicQuestionsRepository> logger,
    IDbConnectionFactory dbConnectionFactory
) : IReadingTopicQuestionsRepository
{
    private readonly ILogger<ReadingTopicQuestionsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<Result<int>> AddQuestionAsync(ReadingTopicQuestion question)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO reading_topic_questions
                    (
                        id,
                        content,
                        serialized_answer_options,
                        topic_id
                    )
                VALUES
                    (
                        @Id,
                        @Content,
                        @SerializedAnswerOptions,
                        @TopicId
                    );
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, question);

        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToCreate,
                "new reading topic question and answer options"
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("add new reading topic question and answer options")
            );
        }

        return totalAffectedRows;
    }
}
