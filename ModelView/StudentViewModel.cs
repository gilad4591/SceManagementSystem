using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ModelView
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
        public Course Course { get; set; }
        public List<Course> Courses { get; set; }
        public Exam Exam { get; set; }
        public List<Exam> Exams { get; set; }

    }
}