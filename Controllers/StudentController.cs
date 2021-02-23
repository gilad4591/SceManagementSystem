using MVC.DbDal;
using MVC.Models;
using MVC.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class StudentController : Controller
    {
        private Dal db = new Dal();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowSchedule()
        {
            Dal dal = new Dal();
            List<Student> objStudent = dal.Students.ToList<Student>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            StudentViewModel svm = new StudentViewModel();
            svm.Courses = objCourse;
            svm.Students = objStudent;
            
            return View(svm);

        }

        public ActionResult ShowExam()
        {
            Dal dal = new Dal();
            List<Student> objStudent = dal.Students.ToList<Student>();
            List<Exam> objExam = dal.Exams.ToList<Exam>();
            StudentViewModel svm = new StudentViewModel();
            svm.Exams = objExam;
            svm.Students = objStudent;

            return View(svm);

        }

    }
}