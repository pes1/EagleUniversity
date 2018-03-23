using System;
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
        public ActionResult Create(DocumentEntity entity)
        {        

            var viewModel = new DocumentViewModel()
            {  assignedEntity=entity, DueDate=DateTime.Now  };

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName");
            return View(viewModel);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentViewModel document, HttpPostedFileBase upload)
        {
            var tempEntity = new DocumentEntity()
            {
                Id = document.assignedEntity.Id,
                 returnTarget= document.assignedEntity.returnTarget,
                 EntityName= document.assignedEntity.EntityName,
                 returnId= document.assignedEntity.returnId,
                 EntityType= document.assignedEntity.EntityType,
                 DocumentTypeName= document.assignedEntity.DocumentTypeName
            };
            document.assignedEntity = tempEntity;

            var addDocument = new Document()
            {
                //Id = document.Id,
                DocumentTypeId = document.DocumentTypeId,
                DocumentContent = document.DocumentContent,
                DocumentName = document.DocumentName,
                DueDate = document.DueDate,
                UploadDate = DateTime.Now
            };

            if (upload != null && upload.ContentLength > 0)
            {

                try
                {
                    if (ModelState.IsValid)

                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            addDocument.DocumentName = System.IO.Path.GetFileName(upload.FileName);
                            addDocument.FileType = upload.ContentType;
                            using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                                addDocument.Content = reader.ReadBytes(upload.ContentLength);
                            }
                        }

                        db.Documents.Add(addDocument);
                        db.SaveChanges();



                        var user = User.Identity.GetUserId();

                        if (document.assignedEntity.EntityType == "Course")
                        {

                            var courseDocument = new CourseDocument()
                            {
                                DocumentId = addDocument.Id,
                                AssignDate = DateTime.Now,
                                OwnerId = User.Identity.GetUserId(),
                                CourseId = document.assignedEntity.Id
                            };

                            db.CourseDocuments.Add(courseDocument);
                            db.SaveChanges();
                        }
                        else if (document.assignedEntity.EntityType == "Module")
                        {
                            var moduleDocument = new ModuleDocument()
                            {
                                DocumentId = addDocument.Id,
                                AssignDate = DateTime.Now,
                                OwnerId = User.Identity.GetUserId(),
                                ModuleId = document.assignedEntity.Id
                            };

                            db.ModuleDocuments.Add(moduleDocument);
                            db.SaveChanges();
                        }
                        else if (document.assignedEntity.EntityType == "Activity")
                        {
                            var activityDocument = new ActivityDocument()
                            {
                                DocumentId = addDocument.Id,
                                AssignDate = DateTime.Now,
                                OwnerId = User.Identity.GetUserId(),
                                ActivityId = document.assignedEntity.Id
                            };

                            db.ActivityDocuments.Add(activityDocument);
                            db.SaveChanges();
                        }



                        return RedirectToAction("Details", "Courses", new { id = document.assignedEntity.returnId, redirect = document.assignedEntity.returnTarget });
                    }
                }
                catch
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }

            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(DocumentEntity entity)
        {

            Document document = db.Documents.Find(entity.Id);

            var viewModel = new DocumentViewModel()
            {Id= document.Id, DocumentContent= document.DocumentContent,
                DocumentName = document.DocumentName,
                UploadDate =document.UploadDate,
                DueDate =document.DueDate,
                DocumentTypeId =document.DocumentTypeId,
                assignedEntity =entity};
            return View(viewModel);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentViewModel document, HttpPostedFileBase upload)
        {
            var tempEntity = new DocumentEntity()
            {
                Id = document.assignedEntity.Id,
                returnTarget = document.assignedEntity.returnTarget,
                EntityName = document.assignedEntity.EntityName,
                returnId = document.assignedEntity.returnId,
                EntityType = document.assignedEntity.EntityType,
                DocumentTypeName = document.assignedEntity.DocumentTypeName
            };
            document.assignedEntity = tempEntity;

            Document documentToEdit = db.Documents.Find(document.Id);

            documentToEdit.DocumentContent = document.DocumentContent;
            documentToEdit.DueDate = document.DueDate;

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    documentToEdit.DocumentName = System.IO.Path.GetFileName(upload.FileName);
                    documentToEdit.FileType = upload.ContentType;
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        documentToEdit.Content = reader.ReadBytes(upload.ContentLength);
                    }
                }

                db.Entry(documentToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Courses", new { id = document.assignedEntity.returnId, redirect = document.assignedEntity.returnTarget });
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        public ActionResult DeleteAjax(DocumentEntity entity)
        {
            return View(entity);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("DeleteAjax")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAjaxConfirmed(DocumentEntity entity)
        {
            Document document = db.Documents.Where(r=>r.Id==entity.Id).SingleOrDefault();

            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Details", "Courses", new { id = entity.returnId, redirect = entity.returnTarget });
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
