using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbConnection.Migrations
{
    /// <inheritdoc />
    public partial class mg04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSPattern",
                columns: table => new
                {
                    SMSPatternId = table.Column<int>(type: "int", nullable: false),
                    SMSPatternName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatternCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSPattern", x => x.SMSPatternId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMSPattern");
        }
    }
}
