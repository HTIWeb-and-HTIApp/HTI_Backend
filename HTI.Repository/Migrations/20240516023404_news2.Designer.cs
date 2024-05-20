﻿// <auto-generated />
using System;
using HTI.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HTI.Repository.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20240516023404_news2")]
    partial class news2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HTI.Core.Entities.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"), 1L, 1);

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AttendanceType")
                        .HasColumnType("bit");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("HTI.Core.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Lap")
                        .HasColumnType("bit");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrerequisiteId")
                        .HasColumnType("int");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<int>("StudyYear")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PrerequisiteId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("HTI.Core.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HTI.Core.Entities.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"), 1L, 1);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("HTI.Core.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("GroupNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LectureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LectureRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxStudentNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("SectionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SectionRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeachingAssistantId")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("TeachingAssistantId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("HTI.Core.Entities.NewsItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoverPhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("HTI.Core.Entities.NewsItemFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsItemId")
                        .HasColumnType("int");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SasUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsItemId");

                    b.ToTable("NewsItemFiles");
                });

            modelBuilder.Entity("HTI.Core.Entities.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationId"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("RegistrationId");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("HTI.Core.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEnrollment")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Expenses")
                        .HasColumnType("real");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("HTI.Core.Entities.StudentCourseHistory", b =>
                {
                    b.Property<int>("StudentCourseHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentCourseHistoryId"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<float>("FinalGrades")
                        .HasColumnType("real");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<float>("MidtermGrades")
                        .HasColumnType("real");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeachingAssistantId")
                        .HasColumnType("int");

                    b.Property<float>("WorkGrades")
                        .HasColumnType("real");

                    b.HasKey("StudentCourseHistoryId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeachingAssistantId");

                    b.ToTable("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.TeachingAssistant", b =>
                {
                    b.Property<int>("TeachingAssistantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeachingAssistantId"), 1L, 1);

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeachingAssistantId");

                    b.ToTable("TeachingAssistants");
                });

            modelBuilder.Entity("HTI.Core.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("HasTeam")
                        .HasColumnType("bit");

                    b.Property<int?>("NumberOfStudents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("HTI.Core.Entities.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("HTI.Core.Entities.TrainingRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("track")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TrainingRegistrations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HTI.Core.Entities.Attendance", b =>
                {
                    b.HasOne("HTI.Core.Entities.Group", "Group")
                        .WithMany("Attendances")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("HTI.Core.Entities.Course", b =>
                {
                    b.HasOne("HTI.Core.Entities.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Course", "Prerequisite")
                        .WithMany()
                        .HasForeignKey("PrerequisiteId");

                    b.Navigation("Department");

                    b.Navigation("Prerequisite");
                });

            modelBuilder.Entity("HTI.Core.Entities.Group", b =>
                {
                    b.HasOne("HTI.Core.Entities.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Doctor", "Doctor")
                        .WithMany("Groups")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.TeachingAssistant", "TeachingAssistant")
                        .WithMany("Groups")
                        .HasForeignKey("TeachingAssistantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Doctor");

                    b.Navigation("TeachingAssistant");
                });

            modelBuilder.Entity("HTI.Core.Entities.NewsItemFile", b =>
                {
                    b.HasOne("HTI.Core.Entities.NewsItem", "NewsItem")
                        .WithMany("Files")
                        .HasForeignKey("NewsItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsItem");
                });

            modelBuilder.Entity("HTI.Core.Entities.Registration", b =>
                {
                    b.HasOne("HTI.Core.Entities.Group", "Group")
                        .WithMany("Registrations")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Student", "Student")
                        .WithMany("Registrations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("HTI.Core.Entities.Student", b =>
                {
                    b.HasOne("HTI.Core.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("HTI.Core.Entities.StudentCourseHistory", b =>
                {
                    b.HasOne("HTI.Core.Entities.Course", "Course")
                        .WithMany("StudentCourseHistories")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Doctor", "Doctor")
                        .WithMany("StudentCourseHistories")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Group", "Group")
                        .WithMany("StudentCourseHistories")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.Student", "Student")
                        .WithMany("StudentCourseHistories")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HTI.Core.Entities.TeachingAssistant", "TeachingAssistant")
                        .WithMany("StudentCourseHistories")
                        .HasForeignKey("TeachingAssistantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Doctor");

                    b.Navigation("Group");

                    b.Navigation("Student");

                    b.Navigation("TeachingAssistant");
                });

            modelBuilder.Entity("HTI.Core.Entities.TeamMember", b =>
                {
                    b.HasOne("HTI.Core.Entities.Team", null)
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HTI.Core.Entities.Course", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("HTI.Core.Entities.Doctor", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.Group", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Registrations");

                    b.Navigation("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.NewsItem", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("HTI.Core.Entities.Student", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Registrations");

                    b.Navigation("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.TeachingAssistant", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("StudentCourseHistories");
                });

            modelBuilder.Entity("HTI.Core.Entities.Team", b =>
                {
                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
