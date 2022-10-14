﻿using Microsoft.EntityFrameworkCore;
using School.Data.TableModels;

namespace School.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                        .HasOne(e => e.Student)
                        .WithMany(s => s.Enrollments)
                        .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                        .HasOne(e => e.Course)
                        .WithMany(c => c.Enrollments)
                        .HasForeignKey(e => e.CourseId);
        }
    }
}
