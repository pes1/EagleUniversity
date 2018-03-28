using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EagleUniversity.Models;
using EagleUniversity.Models.ViewModels;

namespace EagleUniversity.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Partial Stat
        public ActionResult CourseStat(int courseId)
        {

            var role = (from r in db.Roles where r.Name.Contains("Student") select r).FirstOrDefault();
            var students = db.Users
            .Where(x => x.Roles.Select(r => r.RoleId)
            .Contains(role.Id)
            )
            .Where(
            x => x.CourseUserAssigments.Select
            (k => k.CourseId)
            .Contains(courseId))
            .Count();

            var currentActivity = db.Activities.Where(r => r.Modules.CourseId == courseId).Where(k => k.EndDate >= DateTime.Now && k.StartDate <= DateTime.Now).Select(v => v).FirstOrDefault();


            var viewModel = new CourseStatModel()
            { students=students, DocumentName="Not Assigned", DueDate=DateTime.Now, EntityName= "Not Assigned" };

            if (currentActivity!=null)
            {
                viewModel.EntityName = $"Module {currentActivity.Modules.ModuleName}   =>   Activity {currentActivity.ActivityName}";
                var document = db.Documents
                    .Where(d=>d.DocumentTypes.DocumentTypeName.Contains("Task"))
                    .Where(
                    x => x.ActivityDocumentAssignments.Select
                    (k => k.ActivityId)
                    .Contains(currentActivity.Id)).SingleOrDefault();
                if (document!=null)
                {
                    viewModel.DocumentId = document.Id;
                    viewModel.DocumentName = document.DocumentName;
                    viewModel.DueDate = document.DueDate;
                }
                
            }             
            return PartialView("_CourseStat", viewModel);
        }

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id, string redirect = "Default" )
        {
            ViewBag.redirectViewBag = redirect;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            var viewModel = new Course()
            { StartDate = DateTime.Now, EndDate = DateTime.Now };
            return View(viewModel);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseName,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                DateTime e = course.EndDate;
                course.EndDate = new DateTime(e.Year, e.Month, e.Day, 23, 59, 59);
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseName,StartDate,EndDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                DateTime e = course.EndDate;
                course.EndDate = new DateTime(e.Year, e.Month, e.Day, 23, 59, 59);
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
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
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
