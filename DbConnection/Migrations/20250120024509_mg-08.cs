using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "JobTimeType",
                table: "JobAnnouncement",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "JobType",
                table: "JobAnnouncement",
                type: "smallint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTimeType",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "JobType",
                table: "JobAnnouncement");
        }
    }
}
