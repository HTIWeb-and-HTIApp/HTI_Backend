using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTI.Repository.Migrations
{
    public partial class Latestone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Doctors_DoctorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TeachingAssistants_TeachingAssistantId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LectureDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SectionDate",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "GradeLatter",
                table: "StudentCourseHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TeachingAssistantId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SectionRoom",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LectureRoom",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IsOpen",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "LectureDay",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LectureTime",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionDay",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SectionTime",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Doctors_DoctorId",
                table: "Groups",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TeachingAssistants_TeachingAssistantId",
                table: "Groups",
                column: "TeachingAssistantId",
                principalTable: "TeachingAssistants",
                principalColumn: "TeachingAssistantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Doctors_DoctorId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TeachingAssistants_TeachingAssistantId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GradeLatter",
                table: "StudentCourseHistories");

            migrationBuilder.DropColumn(
                name: "LectureDay",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "LectureTime",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SectionDay",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SectionTime",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "TeachingAssistantId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SectionRoom",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LectureRoom",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOpen",
                table: "Groups",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LectureDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SectionDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Doctors_DoctorId",
                table: "Groups",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TeachingAssistants_TeachingAssistantId",
                table: "Groups",
                column: "TeachingAssistantId",
                principalTable: "TeachingAssistants",
                principalColumn: "TeachingAssistantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
