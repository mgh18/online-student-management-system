using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS.Models;
using UserInterface.Controllers;

namespace SMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = db.Teacher.Include(t => t.Role);
            return View(teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Role, "Sysid", "Name");
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sysid,Id,FirstName,LastName,FullName,Email,Role,Photo,Password,CreatedBy,IsDeleted")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                CryptoHash cryptoHash = new CryptoHash();
                string salt = cryptoHash.CreateSalt(10);
                teacher.Password = cryptoHash.GenerateHash(teacher.Password, salt);

                Account account = new Account();
                account.Password = teacher.Password;
                //account.Salt = salt;
                account.TeacherID = teacher.Sysid;
                account.RoleID = teacher.Role;
                //account.IsDeleted = 0;//or isActive
                //account.CreatedBy = Convert.ToInt32(Session["ID"]);
                db.Account.Add(account);
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", teacher.Role);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            //Session["role"] = teacher.Role.Value;
            //Session["createdBy"] = teacher.CreatedBy;
                String r = Session["role"].ToString();
                String c = Session["createdBy"].ToString();
            ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", teacher.Role);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SysId,Id,FirstName,LastName,FullName,Email,CreatedBy,IsDeleted,Role,Photo")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                String r = Session["role"].ToString();
                String c = Session["createdBy"].ToString();
                teacher.Role = Convert.ToInt32(Session["role"].ToString());
                //teacher.CreatedBy = Session["createdBy"].ToString();
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", teacher.Role);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
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
