using Microsoft.EntityFrameworkCore.Migrations;

namespace DbConnection.Migrations
{
    public partial class mg07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EducationalCertificateFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCertificateFirstPageFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityCertificateSecondPageFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCardBackFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalCardFrontFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalImageFileName",
                table: "Applicant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Applicant",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationalCertificateFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "IdentityCertificateFirstPageFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "IdentityCertificateSecondPageFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "NationalCardBackFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "NationalCardFrontFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "PersonalImageFileName",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Applicant");
        }
    }
}
