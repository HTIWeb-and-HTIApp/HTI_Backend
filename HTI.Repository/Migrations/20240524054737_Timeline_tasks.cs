using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTI.Repository.Migrations
{
    public partial class Timeline_tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLine_Groups_GroupId",
                table: "TimeLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeLine",
                table: "TimeLine");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "TimeLine");

            migrationBuilder.RenameTable(
                name: "TimeLine",
                newName: "TimeLines");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "TimeLines",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "TimeLines",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_TimeLine_GroupId",
                table: "TimeLines",
                newName: "IX_TimeLines_GroupId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TimeLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeLines",
                table: "TimeLines",
                column: "TimeLineId");

            migrationBuilder.CreateTable(
                name: "TimeLineFiels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SasUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeLineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLineFiels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeLineFiels_TimeLines_TimeLineId",
                        column: x => x.TimeLineId,
                        principalTable: "TimeLines",
                        principalColumn: "TimeLineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeLineFiels_TimeLineId",
                table: "TimeLineFiels",
                column: "TimeLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLines_Groups_GroupId",
                table: "TimeLines",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeLines_Groups_GroupId",
                table: "TimeLines");

            migrationBuilder.DropTable(
                name: "TimeLineFiels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeLines",
                table: "TimeLines");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TimeLines");

            migrationBuilder.RenameTable(
                name: "TimeLines",
                newName: "TimeLine");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TimeLine",
                newName: "CourseCode");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "TimeLine",
                newName: "Time");

            migrationBuilder.RenameIndex(
                name: "IX_TimeLines_GroupId",
                table: "TimeLine",
                newName: "IX_TimeLine_GroupId");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "TimeLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeLine",
                table: "TimeLine",
                column: "TimeLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLine_Groups_GroupId",
                table: "TimeLine",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
