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
    public class CourseDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseDocuments
        public ActionResult Index()
        {
            var courseDocuments = db.CourseDocuments.Include(c => c.AssignedCourse).Include(c => c.AssignedDocument);
            return View(courseDocuments.ToList());
        }

        // GET: CourseDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDocument courseDocument = db.CourseDocuments.Find(id);
            if (courseDocument == null)
            {
                return HttpNotFound();
            }
            return View(courseDocument);
        }

        // GET: CourseDocuments/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName");
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName");
            return View();
        }

        // POST: CourseDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentId,CourseId,AssignDate,OwnerId")] CourseDocument courseDocument)
        {
            if (ModelState.IsValid)
            {
                db.CourseDocuments.Add(courseDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", courseDocument.CourseId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", courseDocument.DocumentId);
            return View(courseDocument);
        }

        // GET: CourseDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDocument courseDocument = db.CourseDocuments.Find(id);
            if (courseDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", courseDocument.CourseId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", courseDocument.DocumentId);
            return View(courseDocument);
        }

        // POST: CourseDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,CourseId,AssignDate,OwnerId")] CourseDocument courseDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", courseDocument.CourseId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", courseDocument.DocumentId);
            return View(courseDocument);
        }

        // GET: CourseDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseDocument courseDocument = db.CourseDocuments.Find(id);
            if (courseDocument == null)
            {
                return HttpNotFound();
            }
            return View(courseDocument);
        }

        // POST: CourseDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseDocument courseDocument = db.CourseDocuments.Find(id);
            db.CourseDocuments.Remove(courseDocument);
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
