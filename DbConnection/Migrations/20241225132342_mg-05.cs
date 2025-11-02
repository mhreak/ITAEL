using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_User_ApplicationUserId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_ApplicationUserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "HasEmployementExam",
                table: "JobAnnouncement",
                newName: "HasEmploymentExam");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyFieldId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_CityId",
                table: "Applicant",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_StudyFieldId",
                table: "Applicant",
                column: "StudyFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_City_CityId",
                table: "Applicant",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_StudyField_StudyFieldId",
                table: "Applicant",
                column: "StudyFieldId",
                principalTable: "StudyField",
                principalColumn: "StudyFieldId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_City_CityId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_StudyField_StudyFieldId",
                table: "Applicant");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_CityId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_StudyFieldId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "StudyFieldId",
                table: "Applicant");

            migrationBuilder.RenameColumn(
                name: "HasEmploymentExam",
                table: "JobAnnouncement",
                newName: "HasEmployementExam");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Wallet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "User",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_ApplicationUserId",
                table: "Wallet",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_User_ApplicationUserId",
                table: "Wallet",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
