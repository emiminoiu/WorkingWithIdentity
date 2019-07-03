using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Models;

namespace WorkingWithIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CourseStudent>()
             .HasKey(a => new { a.CourseId, a.StudentId });
            builder.Entity<CourseReview>()
             .HasKey(a => new { a.CourseId, a.ReviewId });
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MyUser> MyUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
    }
}
