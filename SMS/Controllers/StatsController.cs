using Microsoft.Ajax.Utilities;
using SMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class StatsController : Controller
    {
        private SMSEntities db = new SMSEntities();

        // GET: Invitation
        public ActionResult Index()
        {
            string role = Session["RoleName"].ToString();
            int id = Convert.ToInt32(Session["ID"]);
          
            IEnumerable<GetStatModel> invitation = null;

            invitation = from Invitation in db.Invitation
                         where Invitation.Teacher_ID == id
                         group new { Invitation.Course, Invitation } by new
                         {
                             Invitation.Course.Name
                         } into g
                         select new GetStatModel
                         {
                                 Name=g.Key.Name,
                                 invited_students = g.Count(p => p.Invitation.Invite != null),
                                 accepted = g.Count(p => p.Invitation.IsAccepted != null),
                                 Percentage = (Double)Math.Round((double)100.0 * g.Count(p => p.Invitation.IsAccepted != null) / g.Count(p => p.Invitation.Invite != null), 1)
                         };


            var maxim = invitation.Max(p => p.Percentage);
            var minim = invitation.Min(p => p.Percentage);
            var popular = invitation.Where(p => p.Percentage == maxim).Select(p => p.Name);
            ViewBag.mostfav = popular.ToList();

            if (maxim != minim)
            {

                var leastfav = invitation.Where(p => p.Percentage == minim).Select(p => p.Name);
                ViewBag.leastfav = leastfav.ToList();
            }
            else if(maxim==0 && minim == 0) 
            {
                var leastfav = "";
                ViewBag.mostfav = leastfav.ToList();
                ViewBag.leastfav = leastfav.ToList();
            }
            else{
                var leastfav = "";
                ViewBag.leastfav = leastfav.ToList();
            }

            return View(invitation.ToList());

        }
    }

   
}