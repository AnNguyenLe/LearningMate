using System.Text.Json;
using Dapper;
using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Listening;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.Entities.Writing;
using LearningMate.Domain.Entities.Speaking;
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
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new exam");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("create new exam"));
        }

        return totalAffectedRows;
    }

    public async Task<Result<bool>> CheckExamExists(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = "SELECT COUNT(DISTINCT 1) from exams WHERE id = @examId";
        var isExist = dbConnection.ExecuteScalar<bool>(sqlQuery, new { examId });

        if (isExist == false)
        {
            _logger.LogWarning(CommonLoggingMessages.RecordNotFoundWithId, nameof(Exam), examId);
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(Exam), examId)
            );
        }

        return isExist;
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

    public async Task<Result<Exam>> GetExamWritingTopicsAsync(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT 
                    e.id as id, e.title as title, e.start_time, e.submission_time,
                    wt.id as id, wt.content, wt.serialized_resources_url as serialized_resources_url, wt.category, wt.score_band
                FROM exams e
                LEFT JOIN writing_topics wt ON wt.exam_id = e.id
                WHERE e.id = @examId;
            """;

        Exam? examResult = null;

        var queryResult = await dbConnection.QueryAsync<Exam, WritingTopic, Exam>(
            sqlQuery,
            (exam, topic) =>
            {
                if (examResult is null)
                {
                    examResult = exam;
                    examResult.WritingTopics = [];
                }

                if (topic is not null)
                {
                    examResult.WritingTopics ??= [];
                    topic.ResourcesUrl = JsonSerializer.Deserialize<List<string>>(
                        topic.SerializedResourcesUrl ?? "[]"
                    );
                    var existingTopic = examResult.WritingTopics.FirstOrDefault(wt =>
                        wt.Id == topic.Id
                    );

                    if (existingTopic is null)
                    {
                        examResult.WritingTopics.Add(topic);
                    }
                }

                return examResult;
            },
            new { examId },
            splitOn: "id"
        );

        var exam = queryResult.FirstOrDefault();
        if (exam is null)
        {
            _logger.LogError(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get reading section of exam",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get reading section of exam")
            );
        }

        return exam;
    }

    public async Task<Result<Exam>> GetExamSpeakingTopicsAsync(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT 
                    e.id as id, e.title as title, e.start_time, e.submission_time,
                    st.id as id, st.content, st.serialized_resources_url as serialized_resources_url, st.category, st.score_band
                FROM exams e
                LEFT JOIN speaking_topics st ON st.exam_id = e.id
                WHERE e.id = @examId;
            """;
        Exam? examResult = null;
        var queryResult = await dbConnection.QueryAsync<Exam, SpeakingTopic, Exam>(
            sqlQuery,
            (exam, topic) =>
            {
                if (examResult is null)
                {
                    examResult = exam;
                    examResult.SpeakingTopics = [];
                }
                if (topic is not null)
                {
                    examResult.SpeakingTopics ??= [];
                    topic.ResourcesUrl = JsonSerializer.Deserialize<List<string>>(
                        topic.SerializedResourcesUrl ?? "[]"
                    );
                    var existingTopic = examResult.SpeakingTopics.FirstOrDefault(st =>
                        st.Id == topic.Id
                    );
                    if (existingTopic is null)
                    {
                        examResult.SpeakingTopics.Add(topic);
                    }
                }
                return examResult;
            },
            new { examId },
            splitOn: "id"
        );
        var exam = queryResult.FirstOrDefault();
        if (exam is null)
        {
            _logger.LogError(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get speaking section of exam",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get speaking section of exam")
            );
        }
        return exam;
    }

    public async Task<Result<Exam>> GetExamListeningTopicsAsync(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT 
                    e.id as id, e.title as title, e.start_time, e.submission_time,
                    lt.id as id, lt.category, lt.title as title, lt.content, lt.score_band,
                    ltq.id as id, ltq.content, ltq.serialized_answer_options
                FROM exams e
                LEFT JOIN listening_topics lt ON lt.exam_id = e.id
                LEFT JOIN listening_topic_questions ltq ON ltq.topic_id = lt.id
                WHERE e.id = @examId;
            """;

        Dictionary<Guid, Exam> examDict = [];

        var queryResult = await dbConnection.QueryAsync<
            Exam,
            ListeningTopic,
            ListeningTopicQuestion,
            Exam
        >(
            sqlQuery,
            (exam, listeningTopic, listeningTopicQuestion) =>
            {
                // If this exam doesn't exist in the dictionary, add it
                if (!examDict.TryGetValue(exam.Id, out var examEntry))
                {
                    examEntry = exam;
                    examEntry.ReadingTopics = [];
                    examDict.Add(exam.Id, examEntry);
                }

                // Add the listening topic if it exists
                if (listeningTopic is not null)
                {
                    examEntry.ListeningTopics ??= [];

                    var topic = examEntry.ListeningTopics.FirstOrDefault(rt =>
                        rt.Id == listeningTopic.Id
                    );

                    if (topic == null)
                    {
                        topic = listeningTopic;
                        topic.Questions = [];
                        examEntry.ListeningTopics.Add(topic);
                    }

                    // Add the question if it exists
                    if (listeningTopicQuestion is not null)
                    {
                        topic.Questions ??= [];
                        topic.Questions.Add(listeningTopicQuestion);
                    }
                }

                return examEntry;
            },
            new { examId },
            splitOn: "id"
        );

        var exam = queryResult.FirstOrDefault();
        if (exam is null)
        {
            _logger.LogError(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get listening section of exam",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get listening section of exam")
            );
        }

        return exam;
    }

    public async Task<Result<Exam>> GetExamReadingTopicsAsync(Guid examId)
    {
        using var dbConnection = await _dbConnectionFactory.CreateConnectionAsync();
        var sqlQuery = """
                SELECT 
                    e.id as id, e.title as title, e.start_time, e.submission_time,
                    rt.id as id, rt.category, rt.title as title, rt.content, rt.score_band,
                    rtq.id as id, rtq.content, rtq.serialized_answer_options
                FROM exams e
                LEFT JOIN reading_topics rt ON rt.exam_id = e.id
                LEFT JOIN reading_topic_questions rtq ON rtq.topic_id = rt.id
                WHERE e.id = @examId;
            """;

        Dictionary<Guid, Exam> examDict = [];

        var queryResult = await dbConnection.QueryAsync<
            Exam,
            ReadingTopic,
            ReadingTopicQuestion,
            Exam
        >(
            sqlQuery,
            (exam, readingTopic, readingTopicQuestion) =>
            {
                // If this exam doesn't exist in the dictionary, add it
                if (!examDict.TryGetValue(exam.Id, out var examEntry))
                {
                    examEntry = exam;
                    examEntry.ReadingTopics = [];
                    examDict.Add(exam.Id, examEntry);
                }

                // Add the reading topic if it exists
                if (readingTopic is not null)
                {
                    examEntry.ReadingTopics ??= [];

                    var topic = examEntry.ReadingTopics.FirstOrDefault(rt =>
                        rt.Id == readingTopic.Id
                    );

                    if (topic == null)
                    {
                        topic = readingTopic;
                        topic.Questions = [];
                        examEntry.ReadingTopics.Add(topic);
                    }

                    // Add the question if it exists
                    if (readingTopicQuestion is not null)
                    {
                        topic.Questions ??= [];
                        topic.Questions.Add(readingTopicQuestion);
                    }
                }

                return examEntry;
            },
            new { examId },
            splitOn: "id"
        );

        var exam = queryResult.FirstOrDefault();
        if (exam is null)
        {
            _logger.LogError(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get reading section of exam",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("get reading section of exam")
            );
        }

        return exam;
    }
}
