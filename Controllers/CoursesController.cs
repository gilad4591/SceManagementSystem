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
    public class CoursesController : Controller
    {
        
        private Dal db = new Dal();
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind (Include = "CourseId,CourseName,Day,Hour,Finish,Class")]Course course)
        {
            Dal dal = new Dal();
            string tempCN = course.CourseName;
            string tempD = course.Day;
            string tempH = course.Hour;
            string tempC = course.Class;
            string tempF = course.Finish;

            List<Course> objCourses = dal.Courses.ToList();
            CourseViewModel cvm = new CourseViewModel();
            cvm.Course = course;
            TimeSpan Tfin;
            TimeSpan Tsta;
            Tfin = TimeSpan.Parse(tempF);
            Tsta = TimeSpan.Parse(tempH);
            TimeSpan TfinExistCourse;
            if (Tfin < Tsta)
            {
                ViewBag.error = "class cannot finish before start";
                return View(cvm);
            }

            foreach (Course x in objCourses)
            {
                    TfinExistCourse = TimeSpan.Parse(x.Finish);
                    if (x.CourseName != tempCN && x.Day == tempD && TfinExistCourse > Tsta && x.Class == tempC)
                {
                    ViewBag.error = "Class Taken,Choose other.";
                    return View(cvm);
                }
                else
                    if (x.CourseName == tempCN && x.Day == tempD && x.Hour == tempH && x.Class == tempC)
                {
                    ViewBag.error = "Course already exist";
                    return View(cvm);
                }
            }

            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cvm);
        }
        public ActionResult Edit(int courseId)
        {
            Course course = db.Courses.Find(courseId);
            return View(course);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,Day,Hour,Finish,Class")] Course course)
        {
            Dal dal = new Dal();
            string tempCN = course.CourseName;
            string tempD = course.Day;
            string tempH = course.Hour;
            string tempC = course.Class;
            List<Course> objCourses = dal.Courses.ToList();
            string tempF = course.Finish;
            TimeSpan Tfin;
            TimeSpan Tsta;
            TimeSpan TfinExistCourse;
            Tfin = TimeSpan.Parse(tempF);
            Tsta = TimeSpan.Parse(tempH);
            if (Tfin < Tsta)
            {
                ViewBag.error = "class cannot finish before start";
                return View(course);
            }
            foreach (Course x in objCourses)
            {
                TfinExistCourse = TimeSpan.Parse(x.Finish);
                if (x.Day == tempD && TfinExistCourse > Tsta && x.Class == tempC)
                {
                    ViewBag.error = "Class Taken,Choose other.";
                    return View(course);
                }
                    if (ModelState.IsValid)
                    {
                        db.Entry(course).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                return View(course);
            }

        public ActionResult Delete(int courseId)
        {
            Course course = db.Courses.Find(courseId);
            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int courseId)
        {
            Course course = db.Courses.Find(courseId);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AssignLecturer()
        {
            Dal dal = new Dal();
            List<User> objUsers = dal.Users.ToList<User>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            LecturerViewModel lvm = new LecturerViewModel();
            lvm.Courses = objCourse;
            lvm.Users = objUsers;
            return View(lvm);
        }
        
        public ActionResult AssignConfirmLecturer()
        {
            return View();
        }   
        [HttpPost]
        public ActionResult AssignConfirmLecturer([Bind(Include = "lecIndex,LecId,LecName,CourseId")] Lecturer lecturer)
        {
            Dal dal = new Dal();
            int tempLecindex = lecturer.lecIndex;
            string tempLecId = lecturer.LecId;
            string tempLn = lecturer.LecName;
            int tempCId = lecturer.CourseId;
            List<Lecturer> objLecturer = dal.Lecturers.ToList();
            List<Course> objCourses = dal.Courses.ToList();
            LecturerViewModel lvm = new LecturerViewModel();
            lvm.Lecturers = objLecturer;
            bool flag = true;


            foreach (Lecturer x in objLecturer)
            {
                if (x.LecId == tempLecId)
                {

                    if (x.CourseId == tempCId)
                    {
                        flag = false;
                        ViewBag.error = "Lecturer already assigned to this course.";
                        return View(lvm);
                    }
                    else
                        if (x.CourseId != tempCId)
                    {
                        Course course = db.Courses.Find(tempCId);
                        if (course != null)
                        {
                            foreach (Course c in objCourses)
                            {
                                TimeSpan Tfin;
                                TimeSpan Tsta;
                                Tfin = TimeSpan.Parse(c.Finish);
                                Tsta = TimeSpan.Parse(course.Hour);
                                if (course.Day == c.Day && Tsta < Tfin && c.CourseId != tempCId)
                                {
                                    flag = false;
                                    ViewBag.error = "Lecturer is busy in this time";
                                    return View(lvm);
                                }
                            }
                        }
                        else
                        {
                            ViewBag.error = "course not exist";
                            return View(lvm);
                        }
                    }
                }
                else
                    if(x.CourseId == tempCId)
                {
                    flag = false;
                    ViewBag.error = "course already lecturerd by other lecturer";
                    return View(lvm);
                }
            }
                if (flag)
                {
                    if (ModelState.IsValid)
                    {
                        db.Lecturers.Add(lecturer);
                        db.SaveChanges();
                        return RedirectToAction("AssignLecturer");
                    }
                }

            ViewBag.error = "Lecturer id not exist in the system";
            return View(lvm);
        }

        public ActionResult AssignStudent()
        {
            Dal dal = new Dal();
            List<User> objUsers = dal.Users.ToList<User>();
            List<Course> objCourse = dal.Courses.ToList<Course>();
            List<Student> objStudent = dal.Students.ToList<Student>();
            CourseViewModel cvm = new CourseViewModel();
            cvm.Courses = objCourse;
            cvm.Students = objStudent;
            cvm.Users = objUsers;
            
            return View(cvm);
        }
        public ActionResult AssignConfirm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AssignConfirm([Bind(Include = "studIndex,StudId,StudName,CourseId")] Student student)
        {

            Dal dal = new Dal();
            int tempStudindex = student.studIndex;
            string tempStudId = student.StudId;
            string tempSn = student.StudName;
            int tempCId = student.CourseId;

            List<Student> objStudents = dal.Students.ToList();
            List<Course> objCourses = dal.Courses.ToList();
            CourseViewModel cvm = new CourseViewModel();
            cvm.Students = objStudents;
            bool flag = false;
            foreach (Student x in objStudents)
            {
                if (x.StudId == tempStudId)
                {
                    flag = true;
                    if (x.CourseId == tempCId)
                    {
                        flag = false;
                        ViewBag.error = "Student already assigned to this course.";
                        return View(cvm);
                    }
                    else
                        if (x.CourseId != tempCId)
                    {
                        Course course = db.Courses.Find(tempCId);
                        if (course != null)
                        {
                            foreach (Course c in objCourses)
                            {
                                TimeSpan Tfin;
                                TimeSpan Tsta;
                                Tfin = TimeSpan.Parse(c.Finish);
                                Tsta = TimeSpan.Parse(course.Hour);
                                if (course.Day == c.Day && Tsta < Tfin && c.CourseId != tempCId)
                                {
                                    flag = false;
                                    ViewBag.error = "student is busy in this time";
                                    return View(cvm);
                                }
                            }
                        }
                        else
                        {
                            ViewBag.error = "Course not exist";
                            return View(cvm);
                        }
                    }
                }
            }
                if (flag)
                {
                    if (ModelState.IsValid)
                    {
                        db.Students.Add(student);
                        db.SaveChanges();
                        return RedirectToAction("AssignStudent");
                    }
                }
            ViewBag.error = "student id not exist in the system";
            return View(cvm);
        }
        public ActionResult EditExam()
        {
            return View(db.Exams.ToList());
        }
        public ActionResult EditExamConfirm(int ExamIndex)
        {
            Exam exam = db.Exams.Find(ExamIndex);
            return View(exam);
        }
        [HttpPost, ActionName("EditExamConfirm")]
        public ActionResult EditExamConfirm([Bind(Include = "ExamIndex,CourseId,StudentId,CourseName,MoedADate,ClassA,MoedBDate,ClassB,Grade")] Exam exam)
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
            DateTime mA;
            DateTime mB;
            mA = DateTime.Parse(exam.MoedADate);
            mB = DateTime.Parse(exam.MoedBDate);
            if (mA > mB)
            {
                ViewBag.error = "moed b must be after moed a";
                return View(exam);
            }
            foreach (Exam x in objExam)
            {
                if (ModelState.IsValid)
                {
                    foreach (Exam d in objExam)
                    {
                        if (d.CourseId == exam.CourseId)
                        {
                            db.Entry(exam).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("EditExam");
                }
            }
            return View(exam);
        }
        public ActionResult EditExamGrade()
        {
            return View(db.Exams.ToList());
        }
        public ActionResult EditExamGradeConfirm(int ExamIndex)
        {
            Exam exam = db.Exams.Find(ExamIndex);
            return View(exam);
        }
        [HttpPost, ActionName("EditExamGradeConfirm")]
        public ActionResult EditExamGradeConfirm([Bind(Include = "ExamIndex,CourseId,StudentId,CourseName,MoedADate,ClassA,MoedBDate,ClassB,Grade")] Exam exam)
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
                    return RedirectToAction("EditExamGrade");
                }
            }
            return View(exam);
        }
        public ActionResult CreateExam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateExam([Bind(Include = "ExamIndex,CourseId,StudentId,CourseName,MoedADate,ClassA,MoedBDate,ClassB,Grade")]Exam exam)
        {
            Dal dal = new Dal();
            int tempCourseId = exam.CourseId;
            string tempCourseName = exam.CourseName;
            string tempMoedADate = exam.MoedADate;
            string tempClassA = exam.ClassA;
            string tempMoedBDate = exam.MoedBDate;
            string tempClassB = exam.ClassB;
            string tempGrade = exam.Grade;

            List<Exam> objExam = dal.Exams.ToList();
            List<Course> objCourse = dal.Courses.ToList();
            List<Student> objStudent = dal.Students.ToList();
            CourseViewModel cvm = new CourseViewModel();
            cvm.Courses = objCourse;
            cvm.Exams = objExam;
            cvm.Students = objStudent;
            bool flag = true;
            bool flag2 = false;
            DateTime mA;
            DateTime mB;
            mA = DateTime.Parse(exam.MoedADate);
            mB = DateTime.Parse(exam.MoedBDate);
            if (mA > mB)
            {
                ViewBag.error = "moed b must be after moed a";
                return View(cvm);
            }

            foreach (Exam x in objExam)
            {
                if (exam.CourseId == x.CourseId)
                {
                    ViewBag.error = "Course already have exam";
                    flag = false;
                    return View(cvm);
                }
            }
            if (flag)
            {
                foreach (Student s in objStudent)
                {
                    if (s.CourseId == tempCourseId)
                    {
                        flag2 = true;
                        exam.StudentId = s.StudId;
                        db.Exams.Add(exam);
                        db.SaveChanges();
                    }
                }
                if (flag2 == false)
                {
                    ViewBag.error = "no student assign to this course";
                    return View(cvm);
                }
                return RedirectToAction("Index");
            }
            return View(cvm);
        }

    }
}
