using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EagleUniversity.Models;
using Microsoft.AspNet.Identity;

namespace EagleUniversity.Controllers
{
    [Authorize]
    public class ActivityDocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityDocuments
        public ActionResult Index()
        {
            var activityDocuments = db.ActivityDocuments.Include(a => a.AssignedActivity).Include(a => a.AssignedDocument);
            return View(activityDocuments.ToList());
        }

        // GET: ActivityDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDocument activityDocument = db.ActivityDocuments.Find(id);
            if (activityDocument == null)
            {
                return HttpNotFound();
            }
            return View(activityDocument);
        }

        // GET: ActivityDocuments/Create
        public ActionResult Create(int DocumentId)
        {
            var alreadyExist = db.ActivityDocuments.Where(r => r.DocumentId == DocumentId);

            var document = db.Documents.Where(r => r.Id == DocumentId).SingleOrDefault();

            if (alreadyExist.Count() > 0)
            {
                return RedirectToAction("Index", "Documents");
            }

            var viewModel = new ActivityDocument()
            { DocumentId = DocumentId, AssignedDocument = document };
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "ActivityName");

            return View(viewModel);
        }


        // POST: ActivityDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentId,ActivityId")] ActivityDocument activityDocument)
        {
            activityDocument.OwnerId = User.Identity.GetUserId();
            activityDocument.AssignDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ActivityDocuments.Add(activityDocument);
                db.SaveChanges();
                return RedirectToAction("Index", "Documents");
            }

            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "ActivityName", activityDocument.ActivityId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", activityDocument.DocumentId);
            return View(activityDocument);
        }

        // GET: ActivityDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDocument activityDocument = db.ActivityDocuments.Find(id);
            if (activityDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "ActivityName", activityDocument.ActivityId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", activityDocument.DocumentId);
            return View(activityDocument);
        }

        // POST: ActivityDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,ActivityId,AssignDate,OwnerId")] ActivityDocument activityDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activities, "Id", "ActivityName", activityDocument.ActivityId);
            ViewBag.DocumentId = new SelectList(db.Documents, "Id", "DocumentName", activityDocument.DocumentId);
            return View(activityDocument);
        }

        // GET: ActivityDocuments/Delete/5
        public ActionResult Delete(int? DocumentId)
        {
            if (DocumentId == null)
            {
                return RedirectToAction("Index", "Documents");
            }
            ActivityDocument activityDocument = db.ActivityDocuments.Where(r => r.DocumentId == DocumentId).SingleOrDefault(); 
            if (activityDocument == null)
            {
                return RedirectToAction("Index", "Documents");
            }
            return View(activityDocument);
        }

        // POST: ActivityDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ActivityDocument document)
        {
            ActivityDocument activityDocument = db.ActivityDocuments.Where(r => r.DocumentId == document.DocumentId).SingleOrDefault();
            db.ActivityDocuments.Remove(activityDocument);
            db.SaveChanges();
            return RedirectToAction("Index", "Documents");
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
