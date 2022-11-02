using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SMS.Models;
using UserInterface.Controllers;

namespace SMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SMSEntities entities = new SMSEntities();
        CryptoHash cryptoHash = new CryptoHash();

        public AccountController()
        {
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView();
        }

        
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Account model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Role role = new Role();
            Account account;
            bool verify = false;
            Role teacher = entities.Role.Where(r => r.Name == "Teacher").FirstOrDefault();
            Role student = entities.Role.Where(r => r.Name == "Student").FirstOrDefault();
            if (model.RoleID == teacher.Sysid)
            {
                account = entities.Account.Where(x => x.Teacher.Id == model.Student.Id).FirstOrDefault();
                if (account != null)
                {
                    if (model.Password== account.Password)
                    {
                        verify = true;
                    }
                    else
                    {
                        verify = false;
                    }
                    
                }
            }

            else if (model.RoleID == student.Sysid)
            {
                account = entities.Account.Where(x => x.Student.Id == model.Student.Id).FirstOrDefault();
                if (account != null)
                {
                    if (model.Password == account.Password)
                    {
                        verify = true;
                    }
                    else
                    {
                        verify = false;
                    }
                    
                }

            }
            else
            {
                account = null;
            }
            if (verify == true)
            {
                if (account.Role.Name == "Teacher")
                {
                    Session["Role"] = "Teacher";
                    Session["ID"] = account.TeacherID;
                    Session["TeacherSysID"] = account.Teacher.Sysid;
                    Session["Name"] = account.Teacher.FullName;
                    Session["Photo"] = account.Teacher.Photo;
                    Session["RoleName"] = account.Role.Name;
                    Session["RoleID"] = account.Role.Sysid;
                    FormsAuthentication.SetAuthCookie(account.Teacher.Id, false);
                    return RedirectToAction("Index", "Home");
                }
                else if (account.Role.Name == "Student")
                {
                    Session["Role"] = "Student";
                    Session["ID"] = account.StudentID;
                    Session["Name"] = account.Student.FullName;
                    Session["Photo"] = account.Student.Photo;
                    Session["RoleName"] = account.Role.Name;
                    Session["RoleID"] = account.Role.Sysid;
                    FormsAuthentication.SetAuthCookie(account.Student.Id, false);
                    return RedirectToAction("Index", "CourseStudent");
                }
               
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return RedirectToAction("Login", "Account");
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

      

      
    }
}