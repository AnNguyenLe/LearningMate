using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnQuestionToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "question",
                table: "reading_topic_questions",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "question",
                table: "listening_topic_questions",
                newName: "content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "content",
                table: "reading_topic_questions",
                newName: "question");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "listening_topic_questions",
                newName: "question");
        }
    }
}
