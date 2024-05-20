using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTI.Repository.Migrations
{
    public partial class Quiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCourseHistoryId = table.Column<int>(type: "int", nullable: false),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuizGrade = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quizzes_StudentCourseHistories_StudentCourseHistoryId",
                        column: x => x.StudentCourseHistoryId,
                        principalTable: "StudentCourseHistories",
                        principalColumn: "StudentCourseHistoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_StudentCourseHistoryId",
                table: "Quizzes",
                column: "StudentCourseHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
