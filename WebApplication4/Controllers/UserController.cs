using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index(string? id)
        {
            var user = db.Users.Find(id);

            db.Entry(user).Reference(p => p.Specification).Load();
            IEnumerable<EnrollmentRequests> er = db.EnrollmentRequests.ToList();
            ViewBag.EnrollmentRequests = er;
            IEnumerable<Specifications> specifications = db.Specifications;
            ViewBag.Specifications = specifications;
            ViewBag.User = user;

            return View();
        }



        [HttpPost]
        public ActionResult Index(string project_title,
            string project_description,
            int specification_id,
            string user_id)
        {
            Projects project = new Projects();
            project.Title = project_title;
            project.Description = project_description;
            project.SpecificationId = specification_id;
            project.PostedById = user_id;
            project.PostedDate = DateTime.Now;

            if (string.IsNullOrEmpty(project.Title))
            {
                ModelState.AddModelError("Title", "Title isn't set");
            }
            else if (project.Title.Length < 5)
            {
                ModelState.AddModelError("Title", "Title should contain at least 6 characters");
            }
            if (string.IsNullOrEmpty(project.Description))
            {
                ModelState.AddModelError("Description", "Description isn't set");
            }
            else if (project.Description.Length < 39)
            {
                ModelState.AddModelError("Description", "Description should contain at least 40 characters");
            }
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Valid";
                db.Projects.Add(project);
                db.SaveChanges();
            }
            ViewBag.Message = "Non Valid";
            var user = db.Users.Find(user_id);

            db.Entry(user).Reference(p => p.Specification).Load();
            IEnumerable<EnrollmentRequests> er = db.EnrollmentRequests.ToList();
            ViewBag.EnrollmentRequests = er;
            IEnumerable<Specifications> specifications = db.Specifications;
            ViewBag.Specifications = specifications;
            ViewBag.User = user;
            return View(project);

        }
    }
}