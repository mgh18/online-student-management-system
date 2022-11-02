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
    public class CourseController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Courses
        public ActionResult Index()
        {
            int TID = Convert.ToInt32(Session["ID"]);

            string role = Session["RoleName"].ToString();
            if (role == "Student" || role=="Teacher")
            {
                int id = Convert.ToInt32(Session["ID"]);
                var courses = db.Teach.Where(x => x.TeacherID== id).Select(x => x.Course);
                //courses = courses.Include(c => c.Department).Include(c => c.Year);
                return View(courses.ToList());
            }
            else
            {
                return (null);
                //var courses = db.Course.Include(c => c.Department).Include(c => c.Year);
                //return View(courses.ToList());
            }
            
        }
        // GET: Courses/Create
        public ActionResult Create()
        {
            
            return View();
        }
        // POST: Courses/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="Sysid,Name,CreatedBy,Day,Time")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.CreatedBy = Convert.ToInt32(Session["ID"]);
                db.Course.Add(course);
                Teach te=new Teach();
                te.TeacherID = Convert.ToInt32(Session["ID"]);
                te.CourseID = course.Sysid;
                db.Teach.Add(te);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(course);
        }
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

 

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name", course.DepartmentID);
            //ViewBag.YearId = new SelectList(db.Years, "SysId", "Value", course.YearId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sysid,Name,Day,Time")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.CreatedBy = Convert.ToInt32(Session["ID"]);
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name", course.DepartmentID);
            //ViewBag.YearId = new SelectList(db.Years, "SysId", "Value", course.YearId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
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
    }
}
