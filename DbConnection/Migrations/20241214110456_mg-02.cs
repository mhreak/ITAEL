using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.ApplicantId);
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
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
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
                name: "ReferralCode",
                columns: table => new
                {
                    ReferralCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferralCodeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RefCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralCode", x => x.ReferralCodeId);
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
                name: "UserLog",
                columns: table => new
                {
                    LogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallet_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobAnnouncement",
                columns: table => new
                {
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    ShowCompanyInfo = table.Column<bool>(type: "bit", nullable: false),
                    HasEmplyementExam = table.Column<bool>(type: "bit", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Applicant_JobAnnouncement",
                columns: table => new
                {
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    JobAnnouncementId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Applicant_JobAnnouncementApplicantId = table.Column<int>(type: "int", nullable: true),
                    Applicant_JobAnnouncementJobAnnouncementId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Applicant_JobAnnouncement_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnounc~",
                        columns: x => new { x.Applicant_JobAnnouncementApplicantId, x.Applicant_JobAnnouncementJobAnnouncementId },
                        principalTable: "Applicant_JobAnnouncement",
                        principalColumns: new[] { "ApplicantId", "JobAnnouncementId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applicant_JobAnnouncement_JobAnnouncement_JobAnnouncementId",
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
                        name: "FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncement_JobAnnouncementId",
                        column: x => x.JobAnnouncementId,
                        principalTable: "JobAnnouncement",
                        principalColumn: "JobAnnouncementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategory_JobAnnouncementCategoryId",
                        column: x => x.JobAnnouncementCategoryId,
                        principalTable: "JobAnnouncementCategory",
                        principalColumn: "JobAnnouncementCategoryId",
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
                    table.PrimaryKey("PK_JobAnnouncement_Skill", x => new { x.JobAnnouncementId, x.SkillId });
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
                    StudyFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAnnouncement_StudyField", x => new { x.JobAnnouncementId, x.StudyFieldId });
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

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_JobAnnouncement_Applicant_JobAnnouncementApplicantId_Applicant_JobAnnouncementJobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                columns: new[] { "Applicant_JobAnnouncementApplicantId", "Applicant_JobAnnouncementJobAnnouncementId" });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_JobAnnouncement_JobAnnouncementId",
                table: "Applicant_JobAnnouncement",
                column: "JobAnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_CompanyId",
                table: "JobAnnouncement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_JobAnnouncementCategory_JobAnnouncementCategoryId",
                table: "JobAnnouncement_JobAnnouncementCategory",
                column: "JobAnnouncementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_Skill_SkillId",
                table: "JobAnnouncement_Skill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_StudyField_StudyFieldId",
                table: "JobAnnouncement_StudyField",
                column: "StudyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ApplicationUserId",
                table: "Wallet",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ReferralCode_CommissionRule_CommissionRuleId",
                table: "Wallet_ReferralCode_CommissionRule",
                column: "CommissionRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ReferralCode_CommissionRule_ReferralCodeId",
                table: "Wallet_ReferralCode_CommissionRule",
                column: "ReferralCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletCommission_WalletId",
                table: "WalletCommission",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant_JobAnnouncement");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_JobAnnouncementCategory");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_Skill");

            migrationBuilder.DropTable(
                name: "JobAnnouncement_StudyField");

            migrationBuilder.DropTable(
                name: "SystemSMS");

            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.DropTable(
                name: "Wallet_ReferralCode_CommissionRule");

            migrationBuilder.DropTable(
                name: "WalletCommission");

            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "JobAnnouncementCategory");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "JobAnnouncement");

            migrationBuilder.DropTable(
                name: "StudyField");

            migrationBuilder.DropTable(
                name: "CommissionRule");

            migrationBuilder.DropTable(
                name: "ReferralCode");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "User",
                newName: "PassengerId");

            migrationBuilder.AddColumn<short>(
                name: "AccountState",
                table: "User",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "BankGateway",
                columns: table => new
                {
                    BankGatewayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    GatewayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GatewayType = table.Column<short>(type: "smallint", nullable: false),
                    Parameter1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankGateway", x => x.BankGatewayId);
                });

            migrationBuilder.CreateTable(
                name: "CarColor",
                columns: table => new
                {
                    CarColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarColor", x => x.CarColorId);
                });

            migrationBuilder.CreateTable(
                name: "CarType",
                columns: table => new
                {
                    CarTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SeatsNumber = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarType", x => x.CarTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "SMS",
                columns: table => new
                {
                    SMSId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalMobiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationRouteStationId = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    GroupOrSingle = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfReceipt = table.Column<int>(type: "int", nullable: false),
                    OrigionRouteStationId = table.Column<int>(type: "int", nullable: true),
                    PassengerId = table.Column<int>(type: "int", nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    ReserveState = table.Column<short>(type: "smallint", nullable: true),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShuttleServiceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS", x => x.SMSId);
                });

            migrationBuilder.CreateTable(
                name: "SMSTemplate",
                columns: table => new
                {
                    SMSTemplateId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSTemplate", x => x.SMSTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarColorId = table.Column<int>(type: "int", nullable: false),
                    CarTypeId = table.Column<int>(type: "int", nullable: false),
                    PelakFirstSegment = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PelakFourthSegment = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PelakSecondSegment = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    PelakThirdSegment = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Car_CarColor_CarColorId",
                        column: x => x.CarColorId,
                        principalTable: "CarColor",
                        principalColumn: "CarColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Car_CarType_CarTypeId",
                        column: x => x.CarTypeId,
                        principalTable: "CarType",
                        principalColumn: "CarTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountState = table.Column<short>(type: "smallint", nullable: false),
                    Adderss_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address_Avenue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address_Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Address_Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DisablityStatus = table.Column<short>(type: "smallint", nullable: false),
                    EndWorkTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    IsShiftWorking = table.Column<bool>(type: "bit", nullable: false),
                    JobTitle = table.Column<short>(type: "smallint", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    PersonnelCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShiftWork1Finish = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork1Start = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork2Finish = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork2Start = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork3Finish = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork3Start = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork4Finish = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ShiftWork4Start = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    StartWorkTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SubOrganizationName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    WorkDay_Friday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Holiday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Monday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Saturday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Sunday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Thursday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    WorkDay_Wednesday = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passenger_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteStation",
                columns: table => new
                {
                    RouteStationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStation", x => x.RouteStationId);
                    table.ForeignKey(
                        name: "FK_RouteStation_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShuttleService",
                columns: table => new
                {
                    ShuttleServiceId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AllowCardToCardPayment = table.Column<bool>(type: "bit", nullable: false),
                    AllowFreeReservation = table.Column<bool>(type: "bit", nullable: false),
                    AllowOnlinePayment = table.Column<bool>(type: "bit", nullable: false),
                    BackAndForth = table.Column<bool>(type: "bit", nullable: false),
                    BankGatewayId = table.Column<int>(type: "int", nullable: true),
                    BankGatewayId2 = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CardNumberToPay = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CardOwnerToPay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gateway1PayPercent = table.Column<short>(type: "smallint", nullable: true),
                    GatewayPay2Percent = table.Column<short>(type: "smallint", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    InFriday = table.Column<bool>(type: "bit", nullable: false),
                    InHolidays = table.Column<bool>(type: "bit", nullable: false),
                    InMonday = table.Column<bool>(type: "bit", nullable: false),
                    InSaturday = table.Column<bool>(type: "bit", nullable: false),
                    InSunday = table.Column<bool>(type: "bit", nullable: false),
                    InThursday = table.Column<bool>(type: "bit", nullable: false),
                    InTuesday = table.Column<bool>(type: "bit", nullable: false),
                    InWednesday = table.Column<bool>(type: "bit", nullable: false),
                    ReservationActive = table.Column<bool>(type: "bit", nullable: false),
                    ReservationFinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReservationStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    SharedOnlinePayment = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShuttleService", x => x.ShuttleServiceId);
                    table.ForeignKey(
                        name: "FK_ShuttleService_BankGateway_BankGatewayId",
                        column: x => x.BankGatewayId,
                        principalTable: "BankGateway",
                        principalColumn: "BankGatewayId");
                    table.ForeignKey(
                        name: "FK_ShuttleService_BankGateway_BankGatewayId2",
                        column: x => x.BankGatewayId2,
                        principalTable: "BankGateway",
                        principalColumn: "BankGatewayId");
                    table.ForeignKey(
                        name: "FK_ShuttleService_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShuttleService_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShuttleService_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreeReservation",
                columns: table => new
                {
                    ShuttleServiceId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    BackAndForthState = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeReservation", x => new { x.ShuttleServiceId, x.PassengerId });
                    table.ForeignKey(
                        name: "FK_FreeReservation_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreeReservation_ShuttleService_ShuttleServiceId",
                        column: x => x.ShuttleServiceId,
                        principalTable: "ShuttleService",
                        principalColumn: "ShuttleServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceReservation",
                columns: table => new
                {
                    ServiceReservationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackAndForthState = table.Column<short>(type: "smallint", nullable: false),
                    DestinationRouteStationId = table.Column<int>(type: "int", nullable: false),
                    FinalAmount = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginRouteStationId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    ReceiptFileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServicePrice = table.Column<int>(type: "int", nullable: false),
                    ShuttleServiceId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    State = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReservation", x => x.ServiceReservationId);
                    table.ForeignKey(
                        name: "FK_ServiceReservation_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceReservation_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceReservation_RouteStation_DestinationRouteStationId",
                        column: x => x.DestinationRouteStationId,
                        principalTable: "RouteStation",
                        principalColumn: "RouteStationId");
                    table.ForeignKey(
                        name: "FK_ServiceReservation_RouteStation_OriginRouteStationId",
                        column: x => x.OriginRouteStationId,
                        principalTable: "RouteStation",
                        principalColumn: "RouteStationId");
                    table.ForeignKey(
                        name: "FK_ServiceReservation_ShuttleService_ShuttleServiceId",
                        column: x => x.ShuttleServiceId,
                        principalTable: "ShuttleService",
                        principalColumn: "ShuttleServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceStation",
                columns: table => new
                {
                    ShuttleServiceId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RouteStationId = table.Column<int>(type: "int", nullable: false),
                    BackAndForthPrice = table.Column<int>(type: "int", nullable: true),
                    BackPrice = table.Column<int>(type: "int", nullable: true),
                    Back_AtTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForthPrice = table.Column<int>(type: "int", nullable: true),
                    Forth_AtTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceStation", x => new { x.ShuttleServiceId, x.RouteStationId });
                    table.ForeignKey(
                        name: "FK_ServiceStation_RouteStation_RouteStationId",
                        column: x => x.RouteStationId,
                        principalTable: "RouteStation",
                        principalColumn: "RouteStationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceStation_ShuttleService_ShuttleServiceId",
                        column: x => x.ShuttleServiceId,
                        principalTable: "ShuttleService",
                        principalColumn: "ShuttleServiceId");
                });

            migrationBuilder.CreateTable(
                name: "OnlineTransaction",
                columns: table => new
                {
                    OnlineTransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BankGatewayId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceReservationId = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<short>(type: "smallint", nullable: false),
                    TransactionCmnt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    VerifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineTransaction", x => x.OnlineTransactionId);
                    table.ForeignKey(
                        name: "FK_OnlineTransaction_BankGateway_BankGatewayId",
                        column: x => x.BankGatewayId,
                        principalTable: "BankGateway",
                        principalColumn: "BankGatewayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineTransaction_ServiceReservation_ServiceReservationId",
                        column: x => x.ServiceReservationId,
                        principalTable: "ServiceReservation",
                        principalColumn: "ServiceReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[] { 4, "رزرو رایگان" });

            migrationBuilder.InsertData(
                table: "SMSTemplate",
                columns: new[] { "SMSTemplateId", "Description", "TemplateName", "TemplateText" },
                values: new object[,]
                {
                    { 1, "[FirstName]: نام، [LastName]: نام خانوادگی", "پیامک ثبت نام موفق در سامانه", "[FirstName] [LastName] عزیز، درخواست شما جهت عضویت در سامانه ثبت شد. درخواست شما توسط شدآمد بررسی شده و نتیجه به شما اطلاع داده خواهد شد" },
                    { 2, "[FirstName]: نام، [LastName]: نام خانوادگی", "پیامک قبول شدن درخواست فعالسازی حساب کاربری", "[FirstName] [LastName] عزیز، حساب کاربری شما فعال شد" },
                    { 3, "[FirstName]: نام، [LastName]: نام خانوادگی", "پیامک رد شدن درخواست فعالسازی حساب کاربری", "[FirstName] [LastName] عزیز، متاسفانه درخواست شما جهت عضویت در سامانه شدآمد رد شد" },
                    { 4, "[FirstName]: نام، [LastName]: نام خانوادگی، [ServiceCode]: کد سرویس، [OriginStationName]: نام ایستگاه مبدا، [DestinationStationName]: نام ایستگاه مقصد", "پیامک رزرو موفق سرویس", "[FirstName] [LastName] عزیز، سرویس [ServiceCode] با موفقیت برای شما رزرو شد. ایستگاه مبدا: [OriginStationName]، ایستگاه مقصد: [DestinationStationName]" }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "SettingId", "SettingKey", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 1, "SMSPanelUsername", "نام کاربری پنل پیامکی", "" },
                    { 2, "SMSPanelPassword", "رمز عبور پنل پیامکی", "" },
                    { 3, "SMSSenderNumber", "شماره ارسال کننده پیامک", "" },
                    { 4, "SendSMSOnSignupSuccess", "ارسال پیامک ثبت نام در سامانه", "" },
                    { 5, "SendSMSOnRequestAccepted", "ارسال پیامک قبول شدن درخواست", "" },
                    { 6, "SendSMSOnRequestRejected", "ارسال پیامک رد شدن درخواست", "" },
                    { 7, "SendSMSOnServiceResered", "ارسال پیامک رزرو موفق سرویس", "" },
                    { 8, "SignupTermsAndCondittions", "شرایط و  قوانین ثبت نام در سامانه", "" },
                    { 9, "ServiceReserveDefaultTermsAndCondittions", "شرایط و قوانین پیش فرض رزرو سرویس", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarColorId",
                table: "Car",
                column: "CarColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarTypeId",
                table: "Car",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FreeReservation_PassengerId",
                table: "FreeReservation",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineTransaction_BankGatewayId",
                table: "OnlineTransaction",
                column: "BankGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineTransaction_ServiceReservationId",
                table: "OnlineTransaction",
                column: "ServiceReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_OrganizationId",
                table: "Passenger",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStation_RouteId",
                table: "RouteStation",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservation_DestinationRouteStationId",
                table: "ServiceReservation",
                column: "DestinationRouteStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservation_OriginRouteStationId",
                table: "ServiceReservation",
                column: "OriginRouteStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservation_PassengerId",
                table: "ServiceReservation",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservation_PaymentTypeId",
                table: "ServiceReservation",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservation_ShuttleServiceId",
                table: "ServiceReservation",
                column: "ShuttleServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceStation_RouteStationId",
                table: "ServiceStation",
                column: "RouteStationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleService_BankGatewayId",
                table: "ShuttleService",
                column: "BankGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleService_BankGatewayId2",
                table: "ShuttleService",
                column: "BankGatewayId2");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleService_CarId",
                table: "ShuttleService",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleService_DriverId",
                table: "ShuttleService",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleService_RouteId",
                table: "ShuttleService",
                column: "RouteId");
        }
    }
}
