using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearningMate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstablishEnglishTestingSkillsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    refresh_token_hash = table.Column<string>(type: "text", nullable: true),
                    refresh_token_expiry_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    submission_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_app_role_claims_app_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "app_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_app_user_claims_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_app_user_logins_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_app_user_roles_app_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "app_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_app_user_roles_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_app_user_tokens_app_users_user_id",
                        column: x => x.user_id,
                        principalTable: "app_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "examinee_exam_relationships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    overall_score = table.Column<double>(type: "double precision", nullable: true),
                    examinee_id = table.Column<Guid>(type: "uuid", nullable: true),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_examinee_exam_relationships", x => x.id);
                    table.ForeignKey(
                        name: "fk_examinee_exam_relationships_app_users_examinee_id",
                        column: x => x.examinee_id,
                        principalTable: "app_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_examinee_exam_relationships_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "listening_topics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    score_band = table.Column<double>(type: "double precision", nullable: true),
                    score = table.Column<double>(type: "double precision", nullable: true),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listening_topics", x => x.id);
                    table.ForeignKey(
                        name: "fk_listening_topics_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reading_topics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    score_band = table.Column<double>(type: "double precision", nullable: true),
                    score = table.Column<double>(type: "double precision", nullable: true),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reading_topics", x => x.id);
                    table.ForeignKey(
                        name: "fk_reading_topics_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "speaking_topics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question = table.Column<string>(type: "text", nullable: true),
                    resources_url = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    score_band = table.Column<double>(type: "double precision", nullable: true),
                    score = table.Column<double>(type: "double precision", nullable: true),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_speaking_topics", x => x.id);
                    table.ForeignKey(
                        name: "fk_speaking_topics_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "writing_topics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question = table.Column<string>(type: "text", nullable: true),
                    resources_url = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: true),
                    score_band = table.Column<double>(type: "double precision", nullable: true),
                    score = table.Column<double>(type: "double precision", nullable: true),
                    exam_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_writing_topics", x => x.id);
                    table.ForeignKey(
                        name: "fk_writing_topics_exams_exam_id",
                        column: x => x.exam_id,
                        principalTable: "exams",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "listening_topic_questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question = table.Column<string>(type: "text", nullable: true),
                    serialized_answer_options = table.Column<string>(type: "text", nullable: true),
                    topic_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_listening_topic_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_listening_topic_questions_listening_topics_topic_id",
                        column: x => x.topic_id,
                        principalTable: "listening_topics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reading_topic_questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    question = table.Column<string>(type: "text", nullable: true),
                    serialized_answer_options = table.Column<string>(type: "text", nullable: true),
                    topic_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reading_topic_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_reading_topic_questions_reading_topics_topic_id",
                        column: x => x.topic_id,
                        principalTable: "reading_topics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "speaking_topic_answers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: true),
                    question_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_speaking_topic_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_speaking_topic_answers_speaking_topics_question_id",
                        column: x => x.question_id,
                        principalTable: "speaking_topics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "writing_topic_answers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    answer = table.Column<string>(type: "text", nullable: true),
                    question_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_writing_topic_answers", x => x.id);
                    table.ForeignKey(
                        name: "fk_writing_topic_answers_writing_topics_question_id",
                        column: x => x.question_id,
                        principalTable: "writing_topics",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_app_role_claims_role_id",
                table: "app_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "app_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_app_user_claims_user_id",
                table: "app_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_app_user_logins_user_id",
                table: "app_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_app_user_roles_role_id",
                table: "app_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "app_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "app_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_examinee_exam_relationships_exam_id",
                table: "examinee_exam_relationships",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_examinee_exam_relationships_examinee_id",
                table: "examinee_exam_relationships",
                column: "examinee_id");

            migrationBuilder.CreateIndex(
                name: "ix_listening_topic_questions_topic_id",
                table: "listening_topic_questions",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_listening_topics_exam_id",
                table: "listening_topics",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_reading_topic_questions_topic_id",
                table: "reading_topic_questions",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "ix_reading_topics_exam_id",
                table: "reading_topics",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_speaking_topic_answers_question_id",
                table: "speaking_topic_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_speaking_topics_exam_id",
                table: "speaking_topics",
                column: "exam_id");

            migrationBuilder.CreateIndex(
                name: "ix_writing_topic_answers_question_id",
                table: "writing_topic_answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_writing_topics_exam_id",
                table: "writing_topics",
                column: "exam_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_role_claims");

            migrationBuilder.DropTable(
                name: "app_user_claims");

            migrationBuilder.DropTable(
                name: "app_user_logins");

            migrationBuilder.DropTable(
                name: "app_user_roles");

            migrationBuilder.DropTable(
                name: "app_user_tokens");

            migrationBuilder.DropTable(
                name: "examinee_exam_relationships");

            migrationBuilder.DropTable(
                name: "listening_topic_questions");

            migrationBuilder.DropTable(
                name: "reading_topic_questions");

            migrationBuilder.DropTable(
                name: "speaking_topic_answers");

            migrationBuilder.DropTable(
                name: "writing_topic_answers");

            migrationBuilder.DropTable(
                name: "app_roles");

            migrationBuilder.DropTable(
                name: "app_users");

            migrationBuilder.DropTable(
                name: "listening_topics");

            migrationBuilder.DropTable(
                name: "reading_topics");

            migrationBuilder.DropTable(
                name: "speaking_topics");

            migrationBuilder.DropTable(
                name: "writing_topics");

            migrationBuilder.DropTable(
                name: "exams");
        }
    }
}
