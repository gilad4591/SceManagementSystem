using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC.Models;
namespace MVC.DbDal
{
    public class Dal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<Exam>().ToTable("Exam");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Exam> Exams { get; set; }
    }
}