using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResource_ExamResourceOrder_ExamResourceOrderId",
                table: "ExamResource");

            migrationBuilder.DropIndex(
                name: "IX_ExamResource_ExamResourceOrderId",
                table: "ExamResource");

            migrationBuilder.DropColumn(
                name: "ExamResourceOrderId",
                table: "ExamResource");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamResourceOrderId",
                table: "ExamResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResource_ExamResourceOrderId",
                table: "ExamResource",
                column: "ExamResourceOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResource_ExamResourceOrder_ExamResourceOrderId",
                table: "ExamResource",
                column: "ExamResourceOrderId",
                principalTable: "ExamResourceOrder",
                principalColumn: "ExamResourceOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
