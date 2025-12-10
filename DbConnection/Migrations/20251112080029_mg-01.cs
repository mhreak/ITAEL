using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "CommissionRule",
                columns: table => new
                {
                    CommissionRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionBasedOn = table.Column<short>(type: "smallint", nullable: false),
                    MinimumAmount = table.Column<long>(type: "bigint", nullable: true),
                    MaximumAmount = table.Column<long>(type: "bigint", nullable: true),
                    MinimumNumber = table.Column<int>(type: "int", nullable: true),
                    MaximumNumber = table.Column<int>(type: "int", nullable: true),
                    CommissionType = table.Column<short>(type: "smallint", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionRule", x => x.CommissionRuleId);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
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
                name: "JobAnnouncementCategory",
                columns: table => new
                {
                    JobAnnouncementCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncementCategory", x => x.JobAnnouncementCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "int", nullable: false),
                    SettingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettingKey = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    SettingValue = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "StudyField",
                columns: table => new
                {
                    StudyFieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyFieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyField", x => x.StudyFieldId);
                });

            migrationBuilder.CreateTable(
                name: "SystemSMS",
                columns: table => new
                {
                    SystemSMSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSType = table.Column<short>(type: "smallint", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSMS", x => x.SystemSMSId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTPExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletId);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar()", maxLength: 3000, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    ShowCompanyInfo = table.Column<bool>(type: "bit", nullable: false),
                    HasEmploymentExam = table.Column<bool>(type: "bit", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    JobType = table.Column<short>(type: "smallint", nullable: true),
                    JobTimeType = table.Column<short>(type: "smallint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement", x => x.JobAnnouncementId);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    ExamQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(MAX)", maxLength: 3000, nullable: false),
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
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
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
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
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
                name: "WalletCommission",
                columns: table => new
                {
                    WalletCommissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    Commission = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletCommission", x => x.WalletCommissionId);
                    table.ForeignKey(
                        name: "FK_WalletCommission_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement_Exam",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    RandomizeQuestions = table.Column<bool>(type: "bit", nullable: false),
                    RandomizeOptions = table.Column<bool>(type: "bit", nullable: false),
                    AllowNavigateToPreviousQuestion = table.Column<bool>(type: "bit", nullable: false)
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
                name: "JobAnnouncement_JobAnnouncementCategory",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    JobAnnouncementCategoryId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_JobAnnouncementCategory", x => new { x.JobAnnouncementId, x.JobAnnouncementCategoryId });
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategory_JobAnnouncementCategoryId",
                        column: x => x.JobAnnouncementCategoryId,
                        principalTable: "JobAnnouncementCategory",
                        principalColumn: "JobAnnouncementCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement_Skill",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    RegistrationAmount = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_Skill", x => new { x.SkillId, x.JobAnnouncementId });
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_Skill_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_Skill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement_StudyField",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    StudyFieldId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_StudyField", x => new { x.StudyFieldId, x.JobAnnouncementId });
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_StudyField_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_StudyField_StudyField_StudyFieldId",
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
                name: "Applicant",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    StudyFieldId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnglishLanguageLevel = table.Column<short>(type: "smallint", nullable: true),
                    PersonalImageFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalCardFrontFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalCardBackFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentityCertificateFirstPageFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdentityCertificateSecondPageFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EducationalCertificateFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.ApplicantId);
                    table.ForeignKey(
                        name: "FK_Applicant_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_StudyField_StudyFieldId",
                        column: x => x.StudyFieldId,
                        principalTable: "StudyField",
                        principalColumn: "StudyFieldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applicant_JobAnnouncement",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant_JobAnnouncement", x => new { x.ApplicantId, x.JobAnnouncementId });
                    table.ForeignKey(
                        name: "FK_Applicant_JobAnnouncement_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_JobAnnouncement_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
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
                    QuestionsOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[,]
                {
                    { 1, "پرداخت آنلاین" },
                    { 2, "پرداخت کارت به کارت" },
                    { 3, "پرداخت از اعتبار کیف پول" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_CityId",
                table: "Applicant",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_StudyFieldId",
                table: "Applicant",
                column: "StudyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_JobAnnouncement_JobAnnouncementId",
                table: "Applicant_JobAnnouncement",
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
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

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
                name: "IX_JobAnnouncement_CompanyId",
                table: "JobAnnouncement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Exam_JobAnnouncementId",
                table: "JobAnnouncement_Exam",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_ExamResource_JobAnnouncementId",
                table: "JobAnnouncement_ExamResource",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategoryId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                column: "JobAnnouncementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Skill_JobAnnouncementId",
                table: "JobAnnouncement_Skill",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_StudyField_JobAnnouncementId",
                table: "JobAnnouncement_StudyField",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ExamResource_ExamResourceId",
                table: "Skill_ExamResource",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyField_ExamResource_ExamResourceId",
                table: "StudyField_ExamResource",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Collaborator_CommissionRule_CollaboratorId",
                table: "Wallet_Collaborator_CommissionRule",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_Collaborator_CommissionRule_CommissionRuleId",
                table: "Wallet_Collaborator_CommissionRule",
                column: "CommissionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletCommission_WalletId",
                table: "WalletCommission",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant_JobAnnouncement");

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
                name: "JobAnnouncement_JobAnnouncementCategory");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_Skill");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_StudyField");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Skill_ExamResource");

            migrationBuilder.DropTable(
                name: "StudyField_ExamResource");

            migrationBuilder.DropTable(
                name: "SystemSMS");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Wallet_Collaborator_CommissionRule");

            migrationBuilder.DropTable(
                name: "WalletCommission");

            migrationBuilder.DropTable(
                name: "ApplicantExamAttempt");

            migrationBuilder.DropTable(
                name: "ExamQuestionOption");

            migrationBuilder.DropTable(
                name: "JobAnnouncementCategory");

            migrationBuilder.DropTable(
                name: "JobAnnouncement");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "ExamResource");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Collaborator");

            migrationBuilder.DropTable(
                name: "CommissionRule");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "StudyField");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
