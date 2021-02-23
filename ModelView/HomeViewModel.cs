using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;

namespace MVC.ModelView
{
    public class HomeViewModel
    {
        public Course course { get; set; }
        public List<Course> Courses { get; set; }
        public Exam exam { get; set; }
        public List<Exam> Exams { get; set; }
    }
}