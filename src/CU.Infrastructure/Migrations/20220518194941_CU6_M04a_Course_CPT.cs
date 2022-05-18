using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CU.Infrastructure.Migrations
{
    public partial class CU6_M04a_Course_CPT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_coursesPresentationTypes",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    CoursePresentationTypesLookupTypeId = table.Column<short>(type: "smallint", nullable: false),
                    CoursePresentationTypesCode = table.Column<string>(type: "nvarchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__coursesPresentationTypes", x => new { x.CourseID, x.CoursePresentationTypesLookupTypeId, x.CoursePresentationTypesCode });
                    table.ForeignKey(
                        name: "FK__coursesPresentationTypes_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__coursesPresentationTypes_xLookups2cKey_CoursePresentationTypesLookupTypeId_CoursePresentationTypesCode",
                        columns: x => new { x.CoursePresentationTypesLookupTypeId, x.CoursePresentationTypesCode },
                        principalTable: "xLookups2cKey",
                        principalColumns: new[] { "LookupTypeId", "Code" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__coursesPresentationTypes_CoursePresentationTypesLookupTypeId_CoursePresentationTypesCode",
                table: "_coursesPresentationTypes",
                columns: new[] { "CoursePresentationTypesLookupTypeId", "CoursePresentationTypesCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_coursesPresentationTypes");
        }
    }
}
