using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            IEnumerable < Specifications > specifications = db.Specifications;
            ViewBag.Specifications = specifications;
            return View();
        }
        public ActionResult Users()
        {
            IEnumerable<ApplicationUser> users= db.Users.Include(p => p.Specification);
            
            return View(users);
        }

        [HttpGet]
        public ActionResult AddSpecification()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSpecification(Specifications specification)
        {
            db.Specifications.Add(specification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            SelectList specifications = new SelectList(db.Specifications, "Id", "Title", "Description");
            ViewBag.Specifications = specifications;
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateUser(ApplicationUser user)
        {
            
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        
        [HttpGet]
        public ActionResult DetailsUser(string? id)
        {
            return View(db.Users.Where(c => c.Id==id).Include(p=>p.Specification).SingleOrDefault());
        }
        
        [HttpGet]
        public ActionResult EditSpecification(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Specifications specification = db.Specifications.Find(id);
            if (specification != null)
            {
                return View(specification);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditSpecification(Specifications specification,string edit)
        {
            if (edit == "Save")
            {
                db.Entry(specification).State = EntityState.Modified;
                db.SaveChanges();
            }
            else if(edit == "Delete")
            {
                db.Entry(specification).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult EditUser(string? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ApplicationUser user= db.Users.Find(id);
            if (user != null)
            {
                SelectList specifications = new SelectList(db.Specifications, "Id", "Title", "Description");
                ViewBag.Specifications = specifications;
                return View(user);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditUser(ApplicationUser user, string edit)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        
        public ActionResult DeleteUser(string? id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult Projects()
        {
            IEnumerable<Projects> projects = db.Projects.Include(p => p.Specification).Include(p => p.PostedBy);
            
            return View(projects);
        }

        [HttpGet]
        public ActionResult CreateProject()
        {

            SelectList users = new SelectList(db.Users,"Id","Name");
            SelectList specifications = new SelectList(db.Specifications,"Id","Title");
            ViewBag.Users = users;
            ViewBag.Specifications = specifications;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(Projects project)
        {
            project.PostedDate = DateTime.Now;
            db.Projects.Add(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }

}