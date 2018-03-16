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
using Microsoft.AspNet.Identity;

namespace EagleUniversity.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.DocumentTypes);
            return View(documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create(int CourseId =0, int ModuleId=0, int ActivityId=0)
        {
            DocumentEntity entity= new DocumentEntity() { EntityType="",Id=0 };


            if (CourseId != 0)
            {

                entity.EntityType = "Course";
                entity.Id = CourseId;
                var course = db.Courses.Where(r => r.Id == (CourseId)).SingleOrDefault();
                entity.EntityName = course.CourseName;
            }
            else if (ModuleId != 0)
            {
                entity.EntityType = "Module";
                entity.Id = ModuleId;
            }
            else if(ActivityId!=0)
            {
                entity.EntityType = "Activity";
                entity.Id = ActivityId;                    
            }            


            var viewModel = new DocumentViewModel()
            {  assignedEntity=entity  };

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName");
            return View(viewModel);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentViewModel document)
        {
            var addDocument = new Document()
            {
                //Id = document.Id,
                DocumentTypeId = document.DocumentTypeId,
                DocumentContent = document.DocumentContent,
                DocumentName = document.DocumentName,
                DueDate = document.DueDate,
                UploadDate = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                db.Documents.Add(addDocument);
                db.SaveChanges();

                var user = User.Identity.GetUserId();

                var courseDocument = new CourseDocument()
                {
                    DocumentId = addDocument.Id,
                    AssignDate = DateTime.Now,
                    OwnerId = User.Identity.GetUserId(),
                    CourseId =document.assignedEntity.Id
                 };

                db.CourseDocuments.Add(courseDocument);
                db.SaveChanges();

                return RedirectToAction("Details", "Courses", new { id = document.assignedEntity.Id, redirect = "Document" });
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DocumentName,DocumentContent,UploadDate,DueDate,DocumentTypeId")] Document document)
        {
            CourseDocument courseDocument = db.CourseDocuments.Where(r => r.DocumentId == document.Id).SingleOrDefault();

            int returnId = 0;
            if (courseDocument != null)
            {
                returnId = courseDocument.CourseId;
            }

            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = returnId, redirect = "Document" });
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAjaxDoc(int id)
        {
            Document document = db.Documents.Where(r=>r.Id==id).SingleOrDefault();
            CourseDocument courseDocument = db.CourseDocuments.Where(r => r.DocumentId == document.Id).SingleOrDefault();

            int returnId=0;
            if (courseDocument!=null)
            {
                returnId = courseDocument.CourseId;
            }

            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Details", "Courses", new { id = returnId, redirect = "Document" });
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
