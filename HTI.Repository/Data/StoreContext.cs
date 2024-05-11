using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;
using HTI.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data;

namespace HTI.Repository.Data
{
    public class StoreContext:IdentityDbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }// allow (DI) by on options
        //Email >> password >>> 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
          .HasOne<Department>(s => s.Department)
          .WithMany(d => d.Students)
          .HasForeignKey(s => s.DepartmentId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
                .HasOne<Student>(r => r.Student)
                .WithMany(s => s.Registrations)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Group)
                .WithMany(g => g.Registrations)
                .HasForeignKey(r => r.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourseHistory>()
                .HasOne<Student>(s => s.Student)
                .WithMany(sc => sc.StudentCourseHistories)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourseHistory>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(c => c.StudentCourseHistories)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourseHistory>()
                .HasOne<Core.Entities.Group>(sc => sc.Group)
                .WithMany(g => g.StudentCourseHistories)
                .HasForeignKey(sc => sc.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourseHistory>()
                .HasOne<Doctor>(sc => sc.Doctor)
                .WithMany(d => d.StudentCourseHistories)
                .HasForeignKey(sc => sc.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourseHistory>()
                .HasOne<TeachingAssistant>(sc => sc.TeachingAssistant)
                .WithMany(ta => ta.StudentCourseHistories)
                .HasForeignKey(sc => sc.TeachingAssistantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attendance>()
                .HasOne<Student>(a => a.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attendance>()
                .HasOne<Core.Entities.Group>(a => a.Group)
                .WithMany(g => g.Attendances)
                .HasForeignKey(a => a.GroupId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrainingRegistration>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.HasTeam).IsRequired();
                entity.Property(e => e.NumberOfStudents).IsRequired(false);
            });
            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.Semester).IsRequired();
            });
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<TeachingAssistant> TeachingAssistants { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Core.Entities.Group> Groups { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<StudentCourseHistory> StudentCourseHistories { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<TrainingRegistration> TrainingRegistrations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

    }
}
