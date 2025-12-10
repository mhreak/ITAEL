using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResourceOrder_ExamResource_ExamResourceId",
                table: "ExamResourceOrder");

            migrationBuilder.DropIndex(
                name: "IX_ExamResourceOrder_ExamResourceId",
                table: "ExamResourceOrder");

            migrationBuilder.DropColumn(
                name: "ExamResourceId",
                table: "ExamResourceOrder");

            migrationBuilder.AddColumn<int>(
                name: "BankGatewayId",
                table: "ExamResourceOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamResourceOrderId",
                table: "ExamResource",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BankGateway",
                columns: table => new
                {
                    BankGatewayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GatewayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GatewayType = table.Column<short>(type: "smallint", nullable: false),
                    Parameter1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Parameter5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankGateway", x => x.BankGatewayId);
                });

            migrationBuilder.CreateTable(
                name: "OnlineTransaction",
                columns: table => new
                {
                    OnlineTransactionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamResourceOrderId = table.Column<int>(type: "int", nullable: false),
                    BankGatewayId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<short>(type: "smallint", nullable: false),
                    TransactionCmnt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineTransaction", x => x.OnlineTransactionId);
                    table.ForeignKey(
                        name: "FK_OnlineTransaction_BankGateway_BankGatewayId",
                        column: x => x.BankGatewayId,
                        principalTable: "BankGateway",
                        principalColumn: "BankGatewayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineTransaction_ExamResourceOrder_ExamResourceOrderId",
                        column: x => x.ExamResourceOrderId,
                        principalTable: "ExamResourceOrder",
                        principalColumn: "ExamResourceOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrder_BankGatewayId",
                table: "ExamResourceOrder",
                column: "BankGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResource_ExamResourceOrderId",
                table: "ExamResource",
                column: "ExamResourceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineTransaction_BankGatewayId",
                table: "OnlineTransaction",
                column: "BankGatewayId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineTransaction_ExamResourceOrderId",
                table: "OnlineTransaction",
                column: "ExamResourceOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResource_ExamResourceOrder_ExamResourceOrderId",
                table: "ExamResource",
                column: "ExamResourceOrderId",
                principalTable: "ExamResourceOrder",
                principalColumn: "ExamResourceOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResourceOrder_BankGateway_BankGatewayId",
                table: "ExamResourceOrder",
                column: "BankGatewayId",
                principalTable: "BankGateway",
                principalColumn: "BankGatewayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResource_ExamResourceOrder_ExamResourceOrderId",
                table: "ExamResource");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResourceOrder_BankGateway_BankGatewayId",
                table: "ExamResourceOrder");

            migrationBuilder.DropTable(
                name: "OnlineTransaction");

            migrationBuilder.DropTable(
                name: "BankGateway");

            migrationBuilder.DropIndex(
                name: "IX_ExamResourceOrder_BankGatewayId",
                table: "ExamResourceOrder");

            migrationBuilder.DropIndex(
                name: "IX_ExamResource_ExamResourceOrderId",
                table: "ExamResource");

            migrationBuilder.DropColumn(
                name: "BankGatewayId",
                table: "ExamResourceOrder");

            migrationBuilder.DropColumn(
                name: "ExamResourceOrderId",
                table: "ExamResource");

            migrationBuilder.AddColumn<int>(
                name: "ExamResourceId",
                table: "ExamResourceOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResourceOrder_ExamResourceId",
                table: "ExamResourceOrder",
                column: "ExamResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResourceOrder_ExamResource_ExamResourceId",
                table: "ExamResourceOrder",
                column: "ExamResourceId",
                principalTable: "ExamResource",
                principalColumn: "ExamResourceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
