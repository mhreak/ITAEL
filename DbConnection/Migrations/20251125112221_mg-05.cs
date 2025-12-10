using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "JobAnnouncement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobAnnouncement_CityId",
                table: "JobAnnouncement",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAnnouncement_City_CityId",
                table: "JobAnnouncement",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAnnouncement_City_CityId",
                table: "JobAnnouncement");

            migrationBuilder.DropIndex(
                name: "IX_JobAnnouncement_CityId",
                table: "JobAnnouncement");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "JobAnnouncement");
        }
    }
}
