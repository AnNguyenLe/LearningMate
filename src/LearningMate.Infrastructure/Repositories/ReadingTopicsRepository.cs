using System.Text.Json;
using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;
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

    public async Task<Result<ReadingTopic>> GetReadingTopicById(Guid id)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT
                    rt.id, rt.content,
                    rtq.id, rtq.content, rtq.serialized_answer_options
                FROM reading_topics rt
                LEFT JOIN reading_topic_questions rtq ON rt.id = rtq.topic_id
                WHERE rt.id = @id;
            """;

        ReadingTopic? readingTopic = null;

        var queryResult = await dbConnection.QueryAsync<
            ReadingTopic,
            ReadingTopicQuestion,
            ReadingTopic
        >(
            sqlQuery,
            (topic, topicQuestion) =>
            {
                readingTopic ??= topic;

                if (topicQuestion is not null)
                {
                    var answerOptions = JsonSerializer.Deserialize<
                        List<MultipleChoiceAnswerOption>
                    >(topicQuestion.SerializedAnswerOptions ?? "[]");

                    if (answerOptions is not null)
                    {
                        topicQuestion.AnswerOptions = answerOptions;
                    }

                    readingTopic.Questions ??= [];
                    readingTopic.Questions.Add(topicQuestion);
                }

                return readingTopic;
            },
            new { id },
            splitOn: "id"
        );

        var readingTopicResult = queryResult.FirstOrDefault();
        if (readingTopicResult is null)
        {
            _logger.LogError(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get reading topic with questions",
                id
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get reading topic with questions")
            );
        }

        return readingTopicResult;
    }
}
