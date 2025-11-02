using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLog");

            migrationBuilder.RenameColumn(
                name: "HasEmplyementExam",
                table: "JobAnnouncement",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ReferralCode",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "JobAnnouncement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "JobAnnouncement",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasEmployementExam",
                table: "JobAnnouncement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Company",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Applicant",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Applicant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ReferralCode");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "HasEmployementExam",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Applicant");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "JobAnnouncement",
                newName: "HasEmplyementExam");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Applicant",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "UserLog",
                columns: table => new
                {
                    LogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLog", x => x.LogId);
                });
        }
    }
}
