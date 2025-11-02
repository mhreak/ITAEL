using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "JobAnnouncement_StudyField",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "JobAnnouncement_StudyField");
        }
    }
}
