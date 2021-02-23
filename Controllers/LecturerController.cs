using MVC.DbDal;
using MVC.Models;
using MVC.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class LecturerController : Controller
    {
        private Dal db = new Dal();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowSchedule()
        {
            Dal dal = new Dal();
            List<Lecturer> objLecturer = dal.Lecturers.ToList<Lecturer>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            List<Student> objStudents = dal.Students.ToList<Student>();
            LecturerViewModel lvm = new LecturerViewModel();
            lvm.Courses = objCourse;
            lvm.Lecturers = objLecturer;
            lvm.Students = objStudents;
            return View(lvm);
        }

        //public ActionResult StudentList()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult StudentList(LecturerViewModel lvm)
        {
            Dal dal = new Dal();
            List<Lecturer> objLecturer = dal.Lecturers.ToList<Lecturer>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            List<Student> objStudents = dal.Students.ToList<Student>();
            //LecturerViewModel lvm = new LecturerViewModel();
            lvm.Students = objStudents;
            lvm.Lecturers = objLecturer;
            lvm.Courses = objCourse;
            //lvm.Course.CourseId = courseId;
            //lvm.Course.CourseName = courseName;
            //Session["SelectedCourseId"] = lvm.Course.CourseId;
            //Session["CourseName"] = lvm.Course.CourseName;

            return View(lvm);
        }

        public ActionResult EditExamGradeLec()
        {
            Dal dal = new Dal();
            List<Lecturer> objLecturer = dal.Lecturers.ToList<Lecturer>();
            List<Exam> objExams = dal.Exams.ToList<Exam>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            List<Student> objStudents = dal.Students.ToList<Student>();
            LecturerViewModel lvm = new LecturerViewModel();
            lvm.Students = objStudents;
            lvm.Exams = objExams;
            lvm.Lecturers = objLecturer;
            lvm.Courses = objCourse;
            return View(lvm);
        }
        public ActionResult EditExamGradeConfirmLec(int ExamIndex)
        {
            Exam exam = db.Exams.Find(ExamIndex);
            LecturerViewModel lvm = new LecturerViewModel();
            return View(exam);
        }
        [HttpPost , ActionName("EditExamGradeConfirmLec")]
        public ActionResult EditExamGradeConfirmLec([Bind(Include = "ExamIndex,CourseId,StudentId,CourseName,MoedADate,ClassA,MoedBDate,ClassB,Grade")] Exam exam)
        {
            Dal dal = new Dal();
            int tempCourseId = exam.CourseId;
            string tempStudentID = exam.StudentId;
            string tempCourseName = exam.CourseName;
            string tempMoedADate = exam.MoedADate;
            string tempClassA = exam.ClassA;
            string tempMoedBDate = exam.MoedBDate;
            string tempClassB = exam.ClassB;
            string tempGrade = exam.Grade;

            List<Exam> objExam = dal.Exams.ToList();
            foreach (Exam x in objExam)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(exam).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("EditExamGradeLec");
                }
            }
            return View(exam);
        }
    }

}