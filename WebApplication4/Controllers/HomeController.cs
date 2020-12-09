using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index()
        {

            IEnumerable<Projects> projects = await db.Projects.Include(p => p.PostedBy).Include(p => p.Specification).ToListAsync();
            ViewBag.Projects = projects.OrderByDescending(p => p.PostedDate);
            IEnumerable<EnrollmentRequests> er = await db.EnrollmentRequests.ToListAsync();
            ViewBag.EnrollmentRequests = er;
            ViewBag.CountRequests = er.Count();
            return View();
        }


        [HttpGet]
        public ActionResult ProjectDetails(int? id)
        {
            IEnumerable<Enrollments> enrollments = db.Enrollments.Where(c => c.ProjectId == id).Include(p => p.User);
            ViewBag.Enrollments = enrollments;
            IEnumerable<EnrollmentRequests> er = db.EnrollmentRequests.ToList();
            ViewBag.EnrollmentRequests = er;
            ViewBag.EnrollmentRequest = db.EnrollmentRequests.Where(c => c.ProjectId == id).SingleOrDefault();
            return View(db.Projects.Where(c => c.Id == id).
                Include(p => p.Specification).Include(p => p.PostedBy).SingleOrDefault());
        }

        public ActionResult RequestToEnroll(int Id)
        {
            var ProjectId = Id;
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.ProjectId = ProjectId;
            IEnumerable<EnrollmentRequests> er = db.EnrollmentRequests.ToList();
            ViewBag.EnrollmentRequests = er;
            return PartialView("RequestToEnroll");
        }

        [HttpPost]
        public ActionResult RequestProcessing(string user_id, int project_id, string accept, string reject)
        {
            EnrollmentRequests er = db.EnrollmentRequests.Where(p => p.ProjectId == project_id && p.UserId == user_id).SingleOrDefault();
            if (er != null)
            {
                Enrollments enrollment = new Enrollments();
                enrollment.UserId = user_id;
                enrollment.ProjectId = project_id;
                if (accept != null)
                {
                    db.Enrollments.Add(enrollment);
                    db.EnrollmentRequests.Remove(er);
                }
                if (reject != null)
                {
                    db.EnrollmentRequests.Remove(er);
                }
            }

            
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ProjectDetails(EnrollmentRequests ers)
        {
            if (string.IsNullOrEmpty(ers.RequestMessage))
            {
                ModelState.AddModelError("RequestMessage", "Message should be set");
            }
            else if (ers.RequestMessage.Length<10)
            {
                ModelState.AddModelError("RequestMessage", "Message should contain at least 10 characters");
            }
            if (ModelState.IsValid)
            {
                db.EnrollmentRequests.Add(ers);
                db.SaveChanges();
            }

            IEnumerable<Enrollments> enrollments = db.Enrollments.Where(c => c.ProjectId == ers.ProjectId).Include(p => p.User);
            ViewBag.Enrollments = enrollments;
            IEnumerable<EnrollmentRequests> er = db.EnrollmentRequests.ToList();
            ViewBag.EnrollmentRequests = er;
            ViewBag.EnrollmentRequest = db.EnrollmentRequests.Where(c => c.ProjectId == ers.ProjectId).SingleOrDefault();
            ViewBag.Enr = ers;
            return View(db.Projects.Where(c => c.Id == ers.ProjectId).
                Include(p => p.Specification).Include(p => p.PostedBy).SingleOrDefault());
           
        }

        [HttpGet]
        public JsonResult CheckName(string message)
        {
            var result = !(message.Length < 5);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}