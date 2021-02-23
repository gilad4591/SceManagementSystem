using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ModelView
{
    public class LecturerViewModel
    {
        public Course Course { get; set; }
        public List<Course> Courses { get; set; }
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; }
        public Exam Exam { get; set; }
        public List<Exam> Exams { get; set; }
        public Lecturer Lecturer { get; set; }
        public List <Lecturer> Lecturers { get; set; }
    }
}