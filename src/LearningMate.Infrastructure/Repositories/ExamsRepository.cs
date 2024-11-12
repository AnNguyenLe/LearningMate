using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using LearningMate.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearningMate.Infrastructure.Repositories;

public class ExamsRepository(
    ILogger<ExamsRepository> logger,
    IDbConnectionFactory dbConnectionFactory,
    ApplicationDbContext applicationDbContext
) : IExamsRepository
{
    private readonly ILogger<ExamsRepository> _logger = logger;
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly ApplicationDbContext _dbContext = applicationDbContext;

    public async Task<Result<int>> AddExamAsync(Exam exam)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlCommand = """
                INSERT INTO exams
                    (
                        id,
                        title,
                        created_at,
                        start_time,
                        submission_time
                    )
                VALUES
                    (
                        @Id,
                        @Title,
                        @CreatedAt,
                        @StartTime,
                        @SubmissionTime
                    );
            """;

        var totalAffectedRows = await dbConnection.ExecuteAsync(sqlCommand, exam);

        if (totalAffectedRows == 0)
        {
            return new ProblemDetailsError("Add new campaign failed.");
        }

        return totalAffectedRows;
    }

    public async Task<Result<Exam>> GetExamOverviewAsync(Guid examId)
    {
        var queryResult = await _dbContext.Exams.FirstOrDefaultAsync(exam => exam.Id == examId);

        if (queryResult is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get exam overview",
                examId
            );
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("get exam overview"));
        }

        return queryResult;
    }

    public async Task<Result<Exam>> GetExamReadingTopicsAsync(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT 
                    e.id as id, e.title as title, e.start_time, e.submission_time,
                    rt.id as id, rt.category, rt.title as title, rt.content, rt.score_band,
                    rtq.id as id, rtq.question, rtq.serialized_answer_options
                FROM exams e
                LEFT JOIN reading_topics rt ON rt.exam_id = e.id
                LEFT JOIN reading_topic_questions rtq ON rtq.topic_id = rt.id
                WHERE e.id = @examId;
            """;

        var queryResult = await dbConnection.QueryAsync<
            Exam,
            ReadingTopic,
            ReadingTopicQuestion,
            Exam
        >(
            sqlQuery,
            (exam, topic, question) =>
            {
                if (topic is null)
                {
                    return exam;
                }

                exam.ReadingTopics ??= [];
                if (!exam.ReadingTopics.Any(readingTopic => readingTopic.Id == topic.Id))
                {
                    exam.ReadingTopics.Add(topic);
                }

                if (question is null)
                {
                    return exam;
                }

                topic.Questions ??= [];
                topic.Questions.Add(question);

                return exam;
            },
            new { examId },
            splitOn: "id"
        );

        var exam = queryResult.FirstOrDefault();

        if (exam is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get exam including reading topics",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get exam including reading topics")
            );
        }

        if (exam.ReadingTopics is null)
        {
            exam.ReadingTopics = [];
            return exam;
        }

        exam.ReadingTopics.ForEach(topic =>
        {
            topic.Questions ??= [];
        });

        return exam;
    }
}
