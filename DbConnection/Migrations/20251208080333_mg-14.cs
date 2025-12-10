using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineTransaction_ExamResourceOrder_ExamResourceOrderId",
                table: "OnlineTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "ExamResourceOrderId",
                table: "OnlineTransaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                table: "OnlineTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobAnnouncementId",
                table: "OnlineTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Applicant_JobAnnouncement",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "ExamResourceOrderItem",
                columns: table => new
                {
                    ExamResourceOrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamResourceId = table.Column<int>(type: "int", nullable: false),
                    ExamResourceOrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResourceOrderItem", x => x.ExamResourceOrderItemId);
                    table.ForeignKey(
                        name: "FK_ExamResourceOrderItem_ExamResourceOrder_ExamResourceOrderId",
                        column: x => x.ExamResourceOrderId,
                        principalTable: "ExamResourceOrder",
                        principalColumn: "ExamResourceOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResourceOrderItem_ExamResource_ExamResourceId",
                        column: x => x.ExamResourceId,
                        principalTable: "ExamResource",
                        principalColumn: "ExamResourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineTransaction_ApplicantId_JobAnnouncementId",
                table: "OnlineTransaction",
                columns: new[] { "ApplicantId", "JobAnnouncementId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrderItem_ExamResourceId",
                table: "ExamResourceOrderItem",
                column: "ExamResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrderItem_ExamResourceOrderId",
                table: "ExamResourceOrderItem",
                column: "ExamResourceOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineTransaction_Applicant_JobAnnouncement_ApplicantId_JobAnnouncementId",
                table: "OnlineTransaction",
                columns: new[] { "ApplicantId", "JobAnnouncementId" },
                principalTable: "Applicant_JobAnnouncement",
                principalColumns: new[] { "ApplicantId", "JobAnnouncementId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineTransaction_ExamResourceOrder_ExamResourceOrderId",
                table: "OnlineTransaction",
                column: "ExamResourceOrderId",
                principalTable: "ExamResourceOrder",
                principalColumn: "ExamResourceOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineTransaction_Applicant_JobAnnouncement_ApplicantId_JobAnnouncementId",
                table: "OnlineTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineTransaction_ExamResourceOrder_ExamResourceOrderId",
                table: "OnlineTransaction");

            migrationBuilder.DropTable(
                name: "ExamResourceOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OnlineTransaction_ApplicantId_JobAnnouncementId",
                table: "OnlineTransaction");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "OnlineTransaction");

            migrationBuilder.DropColumn(
                name: "JobAnnouncementId",
                table: "OnlineTransaction");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Applicant_JobAnnouncement");

            migrationBuilder.AlterColumn<int>(
                name: "ExamResourceOrderId",
                table: "OnlineTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineTransaction_ExamResourceOrder_ExamResourceOrderId",
                table: "OnlineTransaction",
                column: "ExamResourceOrderId",
                principalTable: "ExamResourceOrder",
                principalColumn: "ExamResourceOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
