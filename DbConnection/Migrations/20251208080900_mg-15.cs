using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResourceOrderItem_ExamResource_ExamResourceId",
                table: "ExamResourceOrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResourceOrderItem_ExamResource_ExamResourceId",
                table: "ExamResourceOrderItem",
                column: "ExamResourceId",
                principalTable: "ExamResource",
                principalColumn: "ExamResourceId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResourceOrderItem_ExamResource_ExamResourceId",
                table: "ExamResourceOrderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResourceOrderItem_ExamResource_ExamResourceId",
                table: "ExamResourceOrderItem",
                column: "ExamResourceId",
                principalTable: "ExamResource",
                principalColumn: "ExamResourceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
