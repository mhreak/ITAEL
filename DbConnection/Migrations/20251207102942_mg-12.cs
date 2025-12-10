using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JobAnnouncementApplicationDeadlineDateFrom",
                table: "JobAnnouncement",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JobAnnouncementApplicationDeadlineDateTo",
                table: "JobAnnouncement",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "JobAnnouncement",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShamsiJobAnnouncementApplicationDeadlineDateFrom",
                table: "JobAnnouncement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShamsiJobAnnouncementApplicationDeadlineDateTo",
                table: "JobAnnouncement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Company",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteAddress",
                table: "Company",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobAnnouncementApplicationDeadlineDateFrom",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "JobAnnouncementApplicationDeadlineDateTo",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "ShamsiJobAnnouncementApplicationDeadlineDateFrom",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "ShamsiJobAnnouncementApplicationDeadlineDateTo",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "WebsiteAddress",
                table: "Company");
        }
    }
}
