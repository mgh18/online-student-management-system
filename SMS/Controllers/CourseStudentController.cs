using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS.Models;

namespace SMS.Controllers
{
    [Authorize(Roles = "Student")]
    public class CourseStudentController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: CoursesStudent
        public ActionResult Index()
        {

            string role = Session["RoleName"].ToString();
            int id = Convert.ToInt32(Session["ID"]);
            IEnumerable<Invitation> invitation = null;

            invitation = db.Invitation.Where(x => x.Student_ID == id);
            return View(invitation.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitation invitation = db.Invitation.Find(id);
            if (invitation == null)
            {
                return HttpNotFound();
            }
            invitation.Student_ID = Convert.ToInt32(Session["ID"]);
            invitation.IsAccepted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: accept course/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Sysid,IsAccepted")] Invitation invitation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        invitation.Student_ID= Convert.ToInt32(Session["ID"]);

        //        db.Entry(invitation).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(invitation);
        //}



    } 
}
