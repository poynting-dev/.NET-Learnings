using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }

    public class HomeController : Controller
    {
        [Route("index", Name = "Home Page")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("abt/{id?}", Name = "About Page")]
        public ActionResult About(int id)
        {
            ViewBag.Message = "Your application description page.";
            //dynamic mymodel = new ExpandoObject();
            //ViewModel mymodel = new ViewModel();
            ViewBag.Students = GetStudents();
            ViewBag.Teachers = GetTeachers();
            //var tupleModel = new Tuple<List<Teacher>, List<Student>>(GetTeachers(), GetStudents());

            //mymodel.Teachers = GetTeachers();
            //mymodel.Students = GetStudents();

            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("goto")]
        public RedirectToRouteResult Hello()
        {
            return RedirectToAction("Contact");
        }


        public ActionResult PartialView()
        {
            return View();
        }

        public PartialViewResult Teachers()
        {
            return PartialView(GetTeachers());
        }

        public PartialViewResult Students()
        {
            return PartialView(GetStudents());
        }


        private List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher { TeacherId = 1, Code = "TT", Name = "Tejas Trivedi" });
            teachers.Add(new Teacher { TeacherId = 2, Code = "JT", Name = "Jignesh Trivedi" });
            teachers.Add(new Teacher { TeacherId = 3, Code = "RT", Name = "Rakesh Trivedi" });
            return teachers;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student { StudentId = 1, Code = "L0001", Name = "Amit Gupta", EnrollmentNo = "201404150001" });
            students.Add(new Student { StudentId = 2, Code = "L0002", Name = "Chetan Gujjar", EnrollmentNo = "201404150002" });
            students.Add(new Student { StudentId = 3, Code = "L0003", Name = "Bhavin Patel", EnrollmentNo = "201404150003" });
            return students;
        }
    }
}
