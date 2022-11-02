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
    public class StudentController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Students
        public ActionResult Index()
        {
            int ID = Convert.ToInt32(Session["ID"]);
            //int unRead = db.Messages.Count(x => (x.ToTeachers == ID) && x.Status == 1);
            //Session["unRead"] = unRead;
            int id = Convert.ToInt32(Session["ID"]);
            //var department = db.Teaches.Where(d => d.TeacherId == id).Select(td => td.DepartmentId).ToList();
            //var year = db.Teaches.Where(d => d.TeacherId == id).Select(td => td.YearId);
            //var section = db.Teaches.Where(d => d.TeacherId == id).Select(td => td.SectionId);
            IEnumerable<Student> student=null;
            //var students = db.Students.Include(s => s.Department).Include(s => s.Role1).Include(s => s.Section).Include(s => s.Teacher).Include(s => s.Year);
            if(Session["Role"].ToString() == "Teacher")
            {
                 student = db.Student;
                return View(student.ToList());
            }

            
            if (student != null)
            {
                return View(student.ToList());
            }
            else
            {
                return View();
            }
            
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        //public ActionResult Create()
        //{
        //    int id = Convert.ToInt32(Session["ID"]); 
        //    if (Session["RoleName"].ToString() != "Admin")
        //    {
        //        //ViewBag.DepartmentID = new SelectList(db.Teach.Where(d => d.TeacherID == id).Select(td => td.Department), "SysId", "Name");
        //       // ViewBag.SectionID = new SelectList(db.Teach.Where(d => d.TeacherID == id).Select(td => td.Section), "SysId", "Value");
        //       // ViewBag.YearID = new SelectList(db.Teach.Where(d => d.TeacherID == id).Select(td => td.Year), "SysId", "Value");
        //    }
        //    else
        //    {
        //    //ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name");
        //    //ViewBag.SectionID = new SelectList(db.Sections, "SysId", "Value");
        //    //ViewBag.YearID = new SelectList(db.Years, "SysId", "Value");
        //    }
        //    ViewBag.Role = new SelectList(db.Role, "Sysid", "Name");
        //    ViewBag.CreatedBy = new SelectList(db.Teacher, "Sysid", "Id");

        //    return View();
        //}

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Sysid,Id,FirstName,LastName,FullName,UserName,Email,Phone,Password,Role,Photo")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CryptoHash cryptoHash = new CryptoHash();
        //        string salt = cryptoHash.CreateSalt(10);
        //        student.Password = cryptoHash.GenerateHash(student.Password, salt);

        //        Account account = new Account();
        //        account.Password = student.Password;
        //      //  account.Salt = salt;
        //        account.StudentID = student.Sysid;
        //        account.RoleID = student.Role;
        //        //account.IsDeleted = 0;//or isActive
        //        //account.CreatedBy = Convert.ToInt32(Session["ID"]);

        //        db.Account.Add(account);
        //        db.Student.Add(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name", student.DepartmentID);
        //    ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", student.Role);
        //   // ViewBag.SectionID = new SelectList(db.Sections, "SysId", "Value", student.SectionID);
        //   // ViewBag.CreatedBy = new SelectList(db.Teacher, "SysId", "Id", student.CreatedBy);
        //    //ViewBag.YearID = new SelectList(db.Years, "SysId", "Value", student.YearID);
        //    return View(student);
        //}

        //// GET: Students/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Student.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name", student.DepartmentID);
        //    ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", student.Role);
        //    //ViewBag.SectionID = new SelectList(db.Sections, "SysId", "Value", student.SectionID);
        //    //ViewBag.CreatedBy = new SelectList(db.Teachers, "SysId", "Id", student.CreatedBy);
        //    //ViewBag.YearID = new SelectList(db.Years, "SysId", "Value", student.YearID);
        //    return View(student);
        //}

        //// POST: Students/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Sysid,Id,FirstName,LastName,UserName,Email,Phone,Role,Photo,FullName,CreatedBy")] Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(student).State = EntityState.Modified;
        //        try
        //        {
        //            db.SaveChanges();
        //        }
        //        catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //        {
        //            Exception raise = dbEx;
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    string message = string.Format("{0}:{1}",
        //                        validationErrors.Entry.Entity.ToString(),
        //                        validationError.ErrorMessage);
        //                    // raise a new exception nesting
        //                    // the current instance as InnerException
        //                    raise = new InvalidOperationException(message, raise);
        //                }
        //            }
        //            throw raise;
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.DepartmentID = new SelectList(db.Departments, "SysId", "Name", student.DepartmentID);
        //    ViewBag.Role = new SelectList(db.Role, "Sysid", "Name", student.Role);
        //    //ViewBag.SectionID = new SelectList(db.Sections, "SysId", "Value", student.SectionID);
        //    ViewBag.CreatedBy = new SelectList(db.Teacher, "Sysid", "Id", student.CreatedBy);
        //   // ViewBag.YearID = new SelectList(db.Years, "SysId", "Value", student.YearID);
        //    return View(student);
        //}

        //// GET: Students/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = db.Student.Find(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(student);
        //}

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //    Student student = db.Student.Find(id);
        //    db.Student.Remove(student);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //    }catch(Exception e)
        //    {
        //        ModelState.AddModelError("","This student has ongoing sessions. Please end these sessions before delete");
        //        return View();
        //    }

        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
