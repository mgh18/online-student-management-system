using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS.Models;

namespace SMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class InvitationController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Invitation
        public ActionResult Index()
        {
            string role = Session["RoleName"].ToString();
            int id = Convert.ToInt32(Session["ID"]);
            IEnumerable<Invitation> invitation = null;

            //var courses = db..Where(x => x.TeacherID == id).Select(x => x.Course);
            invitation = db.Invitation.Where(x => x.Teacher_ID == id);
            
            return View(invitation.ToList());
              
          
        }
        // GET: Courses/Create
        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["ID"]);
            ViewBag.Course_Name = new SelectList(db.Teach.Where(x => x.TeacherID == id).Select(x => x.Course), "Sysid", "Name");
            ViewBag.Student_Name = new SelectList(db.Student, "Sysid", "FullName");
            
            return View();
        }

        // POST: Courses/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["ID"]);
                invitation.Teacher_ID = Convert.ToInt32(Session["ID"]);
                //var courses = db.Teach.Where(x => x.TeacherID == id).Select(x => x.Course);
                db.Invitation.Include(s => s.Student).ToList();
                db.Invitation.Include(s => s.Course).ToList();
                invitation.Invite = true;
                db.Invitation.Add(invitation);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                  
                    return RedirectToAction("Index");
                }
               
                return RedirectToAction("Index");

            }

            return View(invitation.ToString());
        }



  
        
    }
}
