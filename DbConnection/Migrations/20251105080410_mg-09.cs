using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_JobAnnouncement_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnounc~",
                table: "Applicant_JobAnnouncement");

            migrationBuilder.DropTable(
                name: "Wallet_ReferralCode_CommissionRule");

            migrationBuilder.DropTable(
                name: "ReferralCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAnnouncement_StudyField",
                table: "JobAnnouncement_StudyField");

            migrationBuilder.DropIndex(
                name: "IX_JobAnnouncement_StudyField_StudyFieldId",
                table: "JobAnnouncement_StudyField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAnnouncement_Skill",
                table: "JobAnnouncement_Skill");

            migrationBuilder.DropIndex(
                name: "IX_JobAnnouncement_Skill_SkillId",
                table: "JobAnnouncement_Skill");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnouncementId",
                table: "Applicant_JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "Applicant_JobAnnouncementApplicantId",
                table: "Applicant_JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "Applicant_JobAnnouncementJobAnnouncementId",
                table: "Applicant_JobAnnouncement");

            migrationBuilder.AlterColumn<int>(
                name: "StudyFieldId",
                table: "JobAnnouncement_StudyField",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_StudyField",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "JobAnnouncement_Skill",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_Skill",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementCategoryId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAnnouncement_StudyField",
                table: "JobAnnouncement_StudyField",
                columns: new[] { "StudyFieldId", "JobAnnouncementId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAnnouncement_Skill",
                table: "JobAnnouncement_Skill",
                columns: new[] { "SkillId", "JobAnnouncementId" });

            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    CollaboratorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferralCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.CollaboratorId);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    RandomizeQuestions = table.Column<bool>(type: "bit", nullable: false),
                    RandomizeOptions = table.Column<bool>(type: "bit", nullable: false),
                    AllowNavigateToPreviousQuestion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamId);
                });

            migrationBuilder.CreateTable(
                name: "ExamResource",
                columns: table => new
                {
                    ExamResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DownloadLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResource", x => x.ExamResourceId);
                });

            migrationBuilder.CreateTable(
                name: "InterviewAppointment",
                columns: table => new
                {
                    InterviewAppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewAppointment", x => x.InterviewAppointmentId);
                    table.ForeignKey(
                        name: "FK_InterviewAppointment_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewAppointment_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet_Collaborator_CommissionRule",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    CollaboratorId = table.Column<int>(type: "int", nullable: false),
                    CommissionRuleId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet_Collaborator_CommissionRule", x => new { x.WalletId, x.CollaboratorId, x.CommissionRuleId });
                    table.ForeignKey(
                        name: "FK_Wallet_Collaborator_CommissionRule_Collaborator_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborator",
                        principalColumn: "CollaboratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_Collaborator_CommissionRule_CommissionRule_CommissionRuleId",
                        column: x => x.CommissionRuleId,
                        principalTable: "CommissionRule",
                        principalColumn: "CommissionRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_Collaborator_CommissionRule_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantExamAttempt",
                columns: table => new
                {
                    ApplicantExamAttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalScore = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantExamAttempt", x => x.ApplicantExamAttemptId);
                    table.ForeignKey(
                        name: "FK_ApplicantExamAttempt_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantExamAttempt_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    ExamQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    QuestionOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.ExamQuestionId);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement_Exam",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_Exam", x => new { x.ExamId, x.JobAnnouncementId });
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_Exam_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_Exam_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamResourceOrder",
                columns: table => new
                {
                    ExamResourceOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ExamResourceId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResourceOrder", x => x.ExamResourceOrderId);
                    table.ForeignKey(
                        name: "FK_ExamResourceOrder_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResourceOrder_ExamResource_ExamResourceId",
                        column: x => x.ExamResourceId,
                        principalTable: "ExamResource",
                        principalColumn: "ExamResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement_ExamResource",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    ExamResourceId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_ExamResource", x => new { x.ExamResourceId, x.JobAnnouncementId });
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_ExamResource_ExamResource_ExamResourceId",
                        column: x => x.ExamResourceId,
                        principalTable: "ExamResource",
                        principalColumn: "ExamResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_ExamResource_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_ExamResource",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ExamResourceId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill_ExamResource", x => new { x.SkillId, x.ExamResourceId });
                    table.ForeignKey(
                        name: "FK_Skill_ExamResource_ExamResource_ExamResourceId",
                        column: x => x.ExamResourceId,
                        principalTable: "ExamResource",
                        principalColumn: "ExamResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_ExamResource_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyField_ExamResource",
                columns: table => new
                {
                    StudyFieldId = table.Column<int>(type: "int", nullable: false),
                    ExamResourceId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyField_ExamResource", x => new { x.StudyFieldId, x.ExamResourceId });
                    table.ForeignKey(
                        name: "FK_StudyField_ExamResource_ExamResource_ExamResourceId",
                        column: x => x.ExamResourceId,
                        principalTable: "ExamResource",
                        principalColumn: "ExamResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyField_ExamResource_StudyField_StudyFieldId",
                        column: x => x.StudyFieldId,
                        principalTable: "StudyField",
                        principalColumn: "StudyFieldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestionOption",
                columns: table => new
                {
                    ExamQuestionOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExamQuestionId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    IsCorrectAnswer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestionOption", x => x.ExamQuestionOptionId);
                    table.ForeignKey(
                        name: "FK_ExamQuestionOption_ExamQuestion_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestion",
                        principalColumn: "ExamQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantExamQuestionAnswer",
                columns: table => new
                {
                    ApplicantExamAttemptId = table.Column<int>(type: "int", nullable: false),
                    ExamQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ExamQuestionOptionId = table.Column<int>(type: "int", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantExamQuestionAnswer", x => new { x.ExamQuestionId, x.ApplicantExamAttemptId });
                    table.ForeignKey(
                        name: "FK_ApplicantExamQuestionAnswer_ApplicantExamAttempt_ApplicantExamAttemptId",
                        column: x => x.ApplicantExamAttemptId,
                        principalTable: "ApplicantExamAttempt",
                        principalColumn: "ApplicantExamAttemptId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantExamQuestionAnswer_ExamQuestionOption_ExamQuestionOptionId",
                        column: x => x.ExamQuestionOptionId,
                        principalTable: "ExamQuestionOption",
                        principalColumn: "ExamQuestionOptionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicantExamQuestionAnswer_ExamQuestion_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestion",
                        principalColumn: "ExamQuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_StudyField_JobAnnouncementId",
                table: "JobAnnouncement_StudyField",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Skill_JobAnnouncementId",
                table: "JobAnnouncement_Skill",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantExamAttempt_ApplicantId",
                table: "ApplicantExamAttempt",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantExamAttempt_ExamId",
                table: "ApplicantExamAttempt",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantExamQuestionAnswer_ApplicantExamAttemptId",
                table: "ApplicantExamQuestionAnswer",
                column: "ApplicantExamAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantExamQuestionAnswer_ExamQuestionOptionId",
                table: "ApplicantExamQuestionAnswer",
                column: "ExamQuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_ExamId",
                table: "ExamQuestion",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestionOption_ExamQuestionId",
                table: "ExamQuestionOption",
                column: "ExamQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrder_ApplicantId",
                table: "ExamResourceOrder",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrder_ExamResourceId",
                table: "ExamResourceOrder",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewAppointment_ApplicantId",
                table: "InterviewAppointment",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewAppointment_JobAnnouncementId",
                table: "InterviewAppointment",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Exam_JobAnnouncementId",
                table: "JobAnnouncement_Exam",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_ExamResource_JobAnnouncementId",
                table: "JobAnnouncement_ExamResource",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ExamResource_ExamResourceId",
                table: "Skill_ExamResource",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyField_ExamResource_ExamResourceId",
                table: "StudyField_ExamResource",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Collaborator_CommissionRule_CollaboratorId",
                table: "Wallet_Collaborator_CommissionRule",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Collaborator_CommissionRule_CommissionRuleId",
                table: "Wallet_Collaborator_CommissionRule",
                column: "CommissionRuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantExamQuestionAnswer");

            migrationBuilder.DropTable(
                name: "ExamResourceOrder");

            migrationBuilder.DropTable(
                name: "InterviewAppointment");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_Exam");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_ExamResource");

            migrationBuilder.DropTable(
                name: "Skill_ExamResource");

            migrationBuilder.DropTable(
                name: "StudyField_ExamResource");

            migrationBuilder.DropTable(
                name: "Wallet_Collaborator_CommissionRule");

            migrationBuilder.DropTable(
                name: "ApplicantExamAttempt");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption");

            migrationBuilder.DropTable(
                name: "ExamResource");

            migrationBuilder.DropTable(
                name: "Collaborator");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAnnouncement_StudyField",
                table: "JobAnnouncement_StudyField");

            migrationBuilder.DropIndex(
                name: "IX_JobAnnouncement_StudyField_JobAnnouncementId",
                table: "JobAnnouncement_StudyField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAnnouncement_Skill",
                table: "JobAnnouncement_Skill");

            migrationBuilder.DropIndex(
                name: "IX_JobAnnouncement_Skill_JobAnnouncementId",
                table: "JobAnnouncement_Skill");

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_StudyField",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudyFieldId",
                table: "JobAnnouncement_StudyField",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_Skill",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "JobAnnouncement_Skill",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementCategoryId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "JobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "Applicant_JobAnnouncementApplicantId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Applicant_JobAnnouncementJobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAnnouncement_StudyField",
                table: "JobAnnouncement_StudyField",
                columns: new[] { "JobAnnouncementId", "StudyFieldId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAnnouncement_Skill",
                table: "JobAnnouncement_Skill",
                columns: new[] { "JobAnnouncementId", "SkillId" });

            migrationBuilder.CreateTable(
                name: "ReferralCode",
                columns: table => new
                {
                    ReferralCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReferralCodeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralCode", x => x.ReferralCodeId);
                });

            migrationBuilder.CreateTable(
                name: "Wallet_ReferralCode_CommissionRule",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    ReferralCodeId = table.Column<int>(type: "int", nullable: false),
                    CommissionRuleId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet_ReferralCode_CommissionRule", x => new { x.WalletId, x.ReferralCodeId, x.CommissionRuleId });
                    table.ForeignKey(
                        name: "FK_Wallet_ReferralCode_CommissionRule_CommissionRule_CommissionRuleId",
                        column: x => x.CommissionRuleId,
                        principalTable: "CommissionRule",
                        principalColumn: "CommissionRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_ReferralCode_CommissionRule_ReferralCode_ReferralCodeId",
                        column: x => x.ReferralCodeId,
                        principalTable: "ReferralCode",
                        principalColumn: "ReferralCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wallet_ReferralCode_CommissionRule_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_StudyField_StudyFieldId",
                table: "JobAnnouncement_StudyField",
                column: "StudyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Skill_SkillId",
                table: "JobAnnouncement_Skill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                columns: new[] { "Applicant_JobAnnouncementApplicantId", "Applicant_JobAnnouncementJobAnnouncementId" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ReferralCode_CommissionRule_CommissionRuleId",
                table: "Wallet_ReferralCode_CommissionRule",
                column: "CommissionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ReferralCode_CommissionRule_ReferralCodeId",
                table: "Wallet_ReferralCode_CommissionRule",
                column: "ReferralCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_JobAnnouncement_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnounc~",
                table: "Applicant_JobAnnouncement",
                columns: new[] { "Applicant_JobAnnouncementApplicantId", "Applicant_JobAnnouncementJobAnnouncementId" },
                principalTable: "Applicant_JobAnnouncement",
                principalColumns: new[] { "ApplicantId", "JobAnnouncementId" });
        }
    }
}
