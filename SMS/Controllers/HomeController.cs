using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    //[Authorize(Roles ="Teachers,Students")]
    public class HomeController : Controller
    {
        private SMSEntities db = new SMSEntities();
       // [Authorize(Roles ="Teachers")]
        public ActionResult Index()
        {
            //count unread messages
            int ID = Convert.ToInt32(Session["ID"]);
            if (Session["RoleName"] != null)
            {
                if (Session["RoleName"].ToString() == "Teacher")
                {
                    GetData();
                    // int unRead = db.Messages.Count(x => (x.ToTeachers == ID) && x.Status == 1);
                    // Session["unRead"] = unRead;
                }
                else
                {
                    //     int unRead = db.Messages.Count(x => (x.ToStudents == ID) && x.Status == 1);
                    //    Session["unRead"] = unRead;
                }
            }

            return View();
        }
        [Authorize(Roles ="Teacher")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetData()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            IEnumerable<Teach> teache = db.Teach.Where(x => x.TeacherID == ID);
                      
            int courses = db.Teach.Where(x => x.TeacherID == ID).Select(x=>x.CourseID).Count();
            
            Report report = new Report();
            report.totalStudents = db.Student.Count();

            report.courses = courses;
        
            ViewBag.totalStudents = db.Student.Count();

            ViewBag.courses = courses;

            return Json(report,JsonRequestBehavior.AllowGet);
        }

        public class Report
        {
            public int totalStudents { get; set; }
        
            public int courses { get; set; }
        
        }
    }
}