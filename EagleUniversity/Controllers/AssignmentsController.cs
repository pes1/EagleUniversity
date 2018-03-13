using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EagleUniversity.Models;

namespace EagleUniversity.Controllers
{
    [Authorize]
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assignments
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.ApplicationUser).Include(a => a.Course);
            return View(assignments.ToList());
        }

        // GET: Assignments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignments assignments = db.Assignments.Find(id);
            if (assignments == null)
            {
                return HttpNotFound();
            }
            return View(assignments);
        }

        // GET: Assignments/Create
        public ActionResult Create(string studentId="", int courseId=0)
        {
            ApplicationUser user= new ApplicationUser();
            if (studentId!="")
            {
                user = db.Users.Where(r => r.Id.Contains(studentId)).SingleOrDefault();
            }
            var alreadyExist = db.Assignments.Where(r => r.ApplicationUserId.Contains(studentId));
            if (alreadyExist.Count()>0)
            {
                return RedirectToAction("Index", "Account", new { userRoleId = "Student" });
            } 

            var viewModel = new Assignments()
            {  ApplicationUserId=studentId, CourseId= courseId, ApplicationUser  = user  };
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName");

            return View(viewModel);
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationUserId,CourseId")] Assignments assignments)
        {
            assignments.AssignDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignments);
                db.SaveChanges();
                return RedirectToAction("Index", "Account", new { userRoleId = "Student" });
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", assignments.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", assignments.CourseId);
            return View(assignments);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignments assignments = db.Assignments.Find(id);
            if (assignments == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", assignments.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", assignments.CourseId);
            return View(assignments);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationUserId,CourseId,AssignDate,OwnerId")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", assignments.ApplicationUserId);
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", assignments.CourseId);
            return View(assignments);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(string studentId = "", int courseId = 0)
        {
            if (studentId == "" || courseId==0)
            {
                return RedirectToAction("Index", "Account", new { userRoleId = "Student" });
            }


            Assignments assignments = db.Assignments.Where(r=>r.CourseId==courseId).Where(r=>r.ApplicationUserId.Contains(studentId)).FirstOrDefault();
            if (assignments == null)
            {
                return RedirectToAction("Index", "Account", new { userRoleId = "Student" });
            }
            return View(assignments);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Assignments assignments)
        {
            Assignments deleteAssigments = db.Assignments.Where(r => r.CourseId == assignments.CourseId).Where(r => r.ApplicationUserId.Contains(assignments.ApplicationUserId)).FirstOrDefault();
            db.Assignments.Remove(deleteAssigments);
            db.SaveChanges();
            return RedirectToAction("Index", "Account", new { userRoleId = "Student" });
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
