using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CU.Infrastructure.Migrations
{
    public partial class CU6_M04b_Department_DFT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_departmentsFacilityTypes",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    LookupTypeId = table.Column<short>(type: "smallint", nullable: false),
                    DepartmentFacilityTypeCode = table.Column<string>(type: "nvarchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departmentsFacilityTypes", x => new { x.DepartmentID, x.LookupTypeId, x.DepartmentFacilityTypeCode });
                    table.ForeignKey(
                        name: "FK__departmentsFacilityTypes_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__departmentsFacilityTypes_xLookups2cKey_LookupTypeId_DepartmentFacilityTypeCode",
                        columns: x => new { x.LookupTypeId, x.DepartmentFacilityTypeCode },
                        principalTable: "xLookups2cKey",
                        principalColumns: new[] { "LookupTypeId", "Code" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__departmentsFacilityTypes_LookupTypeId_DepartmentFacilityTypeCode",
                table: "_departmentsFacilityTypes",
                columns: new[] { "LookupTypeId", "DepartmentFacilityTypeCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_departmentsFacilityTypes");
        }
    }
}
