using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Listening;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.Entities.Speaking;
using LearningMate.Domain.Entities.Writing;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningMate.Infrastructure.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<ExamineeExamRelationship> ExamineeExamRelationships { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<ReadingTopic> ReadingTopics { get; set; }
    public DbSet<ReadingTopicQuestion> ReadingTopicQuestions { get; set; }
    public DbSet<ListeningTopic> ListeningTopics { get; set; }
    public DbSet<ListeningTopicQuestion> ListeningTopicQuestions { get; set; }
    public DbSet<WritingTopic> WritingTopics { get; set; }
    public DbSet<WritingTopicAnswer> WritingTopicAnswers { get; set; }
    public DbSet<SpeakingTopic> SpeakingTopics { get; set; }
    public DbSet<SpeakingTopicAnswer> SpeakingTopicAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(b =>
        {
            b.ToTable("app_users");
        });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("app_user_claims");
        });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("app_user_logins");
        });

        modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("app_user_tokens");
        });

        modelBuilder.Entity<AppRole>(b =>
        {
            b.ToTable("app_roles");
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("app_role_claims");
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("app_user_roles");
        });

        // app_users table
        modelBuilder.Entity<AppUser>().HasKey(user => user.Id);
        modelBuilder.Entity<AppUser>().Ignore(user => user.ExamineeExamRelationships);

        // exams table
        modelBuilder.Entity<Exam>().HasKey(exam => exam.Id);
        modelBuilder
            .Entity<Exam>()
            .Ignore(exam => exam.ExamineeExamRelationships)
            .Ignore(exam => exam.ReadingTopics)
            .Ignore(exam => exam.ListeningTopics)
            .Ignore(exam => exam.WritingTopics);

        // examinee_exam_relationships table
        modelBuilder.Entity<ExamineeExamRelationship>().HasKey(ee => ee.Id);
        modelBuilder
            .Entity<ExamineeExamRelationship>()
            .Ignore(ee => ee.Examinee)
            .Ignore(ee => ee.Exam);
        modelBuilder
            .Entity<ExamineeExamRelationship>()
            .HasOne(ee => ee.Examinee)
            .WithMany(examinee => examinee.ExamineeExamRelationships)
            .HasForeignKey(ee => ee.ExamineeId);
        modelBuilder
            .Entity<ExamineeExamRelationship>()
            .HasOne(ee => ee.Exam)
            .WithMany(exam => exam.ExamineeExamRelationships)
            .HasForeignKey(ee => ee.ExamId);

        // reading_topics table
        modelBuilder.Entity<ReadingTopic>().HasKey(rt => rt.Id);
        modelBuilder.Entity<ReadingTopic>().Ignore(rt => rt.Questions);
        modelBuilder
            .Entity<ReadingTopic>()
            .HasOne(rt => rt.Exam)
            .WithMany(exam => exam.ReadingTopics)
            .HasForeignKey(rt => rt.ExamId);

        // reading_topic_questions table
        modelBuilder.Entity<ReadingTopicQuestion>().HasKey(rtq => rtq.Id);
        modelBuilder
            .Entity<ReadingTopicQuestion>()
            .Ignore(rtq => rtq.Topic)
            .Ignore(rtq => rtq.AnswerOptions);
        modelBuilder
            .Entity<ReadingTopicQuestion>()
            .HasOne(rtq => rtq.Topic)
            .WithMany(topic => topic.Questions)
            .HasForeignKey(rtq => rtq.TopicId);

        // listening_topics table
        modelBuilder.Entity<ListeningTopic>().HasKey(lt => lt.Id);
        modelBuilder.Entity<ListeningTopic>().Ignore(lt => lt.Questions);
        modelBuilder
            .Entity<ListeningTopic>()
            .HasOne(lt => lt.Exam)
            .WithMany(exam => exam.ListeningTopics)
            .HasForeignKey(lt => lt.ExamId);

        // listening_topic_questions table
        modelBuilder.Entity<ListeningTopicQuestion>().HasKey(ltq => ltq.Id);
        modelBuilder
            .Entity<ListeningTopicQuestion>()
            .Ignore(ltq => ltq.AnswerOptions)
            .Ignore(ltq => ltq.Topic);
        modelBuilder
            .Entity<ListeningTopicQuestion>()
            .HasOne(ltq => ltq.Topic)
            .WithMany(topic => topic.Questions)
            .HasForeignKey(ltq => ltq.TopicId);

        // writing_topics table
        modelBuilder.Entity<WritingTopic>().HasKey(wt => wt.Id);
        modelBuilder.Entity<WritingTopic>().Ignore(wt => wt.ResourcesUrl);
        modelBuilder.Entity<WritingTopic>().Ignore(wt => wt.Answers).Ignore(wt => wt.Exam);
        modelBuilder
            .Entity<WritingTopic>()
            .HasOne(wt => wt.Exam)
            .WithMany(exam => exam.WritingTopics)
            .HasForeignKey(wt => wt.ExamId);

        // writing_topic_answers table
        modelBuilder.Entity<WritingTopicAnswer>().HasKey(wta => wta.Id);
        modelBuilder.Entity<WritingTopicAnswer>().Ignore(wta => wta.Topic);
        modelBuilder
            .Entity<WritingTopicAnswer>()
            .HasOne(wta => wta.Topic)
            .WithMany(question => question.Answers)
            .HasForeignKey(wta => wta.TopicId);

        // speaking_topics table
        modelBuilder.Entity<SpeakingTopic>().HasKey(st => st.Id);
        modelBuilder.Entity<SpeakingTopic>().Ignore(st => st.ResourcesUrl);
        modelBuilder.Entity<SpeakingTopic>().Ignore(st => st.Answers).Ignore(st => st.Exam);
        modelBuilder
            .Entity<SpeakingTopic>()
            .HasOne(st => st.Exam)
            .WithMany(exam => exam.SpeakingTopics)
            .HasForeignKey(st => st.ExamId);

        // speaking_topic_answers table
        modelBuilder.Entity<SpeakingTopicAnswer>().HasKey(sta => sta.Id);
        modelBuilder.Entity<SpeakingTopicAnswer>().Ignore(sta => sta.Topic);
        modelBuilder
            .Entity<SpeakingTopicAnswer>()
            .HasOne(sta => sta.Topic)
            .WithMany(question => question.Answers)
            .HasForeignKey(sta => sta.TopicId);
    }
}
