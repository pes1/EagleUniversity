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
        public ActionResult Create(int CourseId =0, int ModuleId=0, int ActivityId=0)
        {
            DocumentEntity entity= new DocumentEntity() { EntityType="",Id=0 };


            if (CourseId != 0)
            {

                entity.EntityType = "Course";
                entity.Id = CourseId;
                var course = db.Courses.Where(r => r.Id == (CourseId)).SingleOrDefault();
                entity.EntityName = course.CourseName;
                entity.returnId = CourseId;
                entity.returnTarget = "Document";
            }
            else if (ModuleId != 0)
            {
                entity.EntityType = "Module";
                entity.Id = ModuleId;
                var module = db.Modules.Where(r => r.Id == (ModuleId)).SingleOrDefault();
                entity.EntityName = module.ModuleName;
                entity.returnId = module.CourseId;
                entity.returnTarget = "Default";
            }
            else if(ActivityId!=0)
            {
                entity.EntityType = "Activity";
                entity.Id = ActivityId;
                var activity = db.Activities.Where(r => r.Id == (ActivityId)).SingleOrDefault();
                entity.EntityName = activity.ActivityName;
                entity.returnId = activity.Modules.CourseId;
                entity.returnTarget = "Default";
            }            


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

            DocumentEntity entity = new DocumentEntity() { EntityType = "", Id = 0 };

            var assignedCourse = db.CourseDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();
            var assignedModule = db.ModuleDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();
            var assignedActivity = db.ActivityDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();

            if (assignedCourse != null)
            {
                entity.EntityType = "Course";
                entity.Id = assignedCourse.CourseId;
                entity.EntityName = assignedCourse.AssignedCourse.CourseName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId= assignedCourse.CourseId;
                entity.returnTarget = "Document";
            }
            else if (assignedModule != null)
            {
                entity.EntityType = "Module";
                entity.Id = assignedModule.ModuleId;
                entity.EntityName = assignedModule.AssignedModule.ModuleName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId = assignedModule.AssignedModule.CourseId;
                entity.returnTarget = "Default";
            }
            else if (assignedActivity != null)
            {
                entity.EntityType = "Activity";
                entity.Id = assignedActivity.ActivityId;
                entity.EntityName = assignedActivity.AssignedActivity.ActivityName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId = assignedActivity.AssignedActivity.Modules.CourseId;
                entity.returnTarget = "Default";
            }


            var viewModel = new DocumentViewModel()
            { Id= document.Id, DocumentContent= document.DocumentContent, DocumentName= document.DocumentName, UploadDate=document.UploadDate, DueDate=document.DueDate
            , DocumentTypeId=document.DocumentTypeId, assignedEntity=entity};

            return View(viewModel);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentViewModel document, HttpPostedFileBase upload)
        {

            Document documentToEdit = db.Documents.Find(document.Id);

            //documentToEdit.DocumentName = document.DocumentName;
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

            DocumentEntity entity = new DocumentEntity() { EntityType = "", Id = 0 };

            var assignedCourse = db.CourseDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();
            var assignedModule = db.ModuleDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();
            var assignedActivity = db.ActivityDocuments.Where(r => r.DocumentId == (document.Id)).SingleOrDefault();

            if (assignedCourse != null)
            {
                entity.EntityType = "Course";
                entity.Id = assignedCourse.CourseId;
                entity.EntityName = assignedCourse.AssignedCourse.CourseName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId = assignedCourse.CourseId;
                entity.returnTarget = "Document";
            }
            else if (assignedModule != null)
            {
                entity.EntityType = "Module";
                entity.Id = assignedModule.ModuleId;
                entity.EntityName = assignedModule.AssignedModule.ModuleName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId = assignedModule.AssignedModule.CourseId;
                entity.returnTarget = "Default";
            }
            else if (assignedActivity != null)
            {
                entity.EntityType = "Activity";
                entity.Id = assignedActivity.ActivityId;
                entity.EntityName = assignedActivity.AssignedActivity.ActivityName;
                entity.DocumentTypeName = document.DocumentTypes.DocumentTypeName;
                entity.returnId = assignedActivity.AssignedActivity.Modules.CourseId;
                entity.returnTarget = "Default";
            }

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
