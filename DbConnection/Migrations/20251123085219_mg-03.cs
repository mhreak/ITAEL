using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_City_CityId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_StudyField_StudyFieldId",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "StudyFieldId",
                table: "Applicant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Applicant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Applicant",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_City_CityId",
                table: "Applicant",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_StudyField_StudyFieldId",
                table: "Applicant",
                column: "StudyFieldId",
                principalTable: "StudyField",
                principalColumn: "StudyFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_City_CityId",
                table: "Applicant");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_StudyField_StudyFieldId",
                table: "Applicant");

            migrationBuilder.AlterColumn<int>(
                name: "StudyFieldId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Applicant",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
