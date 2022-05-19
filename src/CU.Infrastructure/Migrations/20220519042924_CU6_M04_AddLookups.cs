using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CU.Infrastructure.Migrations
{
    public partial class CU6_M04_AddLookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "xLookups2cKey",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    LookupTypeId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    _SubType = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xLookups2cKey", x => new { x.LookupTypeId, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "xLookupTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xLookupTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "LookupTypeAndName",
                table: "xLookups2cKey",
                columns: new[] { "LookupTypeId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "xLookups2cKey");

            migrationBuilder.DropTable(
                name: "xLookupTypes");
        }
    }
}
