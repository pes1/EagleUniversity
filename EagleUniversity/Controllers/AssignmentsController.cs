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

        // GET: Assignments/CreateAjax
        public ActionResult CreateAjax(UserEntity userEntity)
        {
            var userId = userEntity.UserId;            
            var alreadyExist = db.Assignments.Where(r => r.ApplicationUserId.Contains(userId));
            if (alreadyExist.Count() > 0)
            {
                return RedirectToAction(userEntity.returnMethod, userEntity.returnController, new { id = userEntity.returnId, redirect = userEntity.returnTarget });
            }

            var viewModel = new AssignmentsViewModel()
            {  assignedPropety=userEntity };

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
        //Post CreateAjax
        [HttpPost, ActionName("CreateAjax")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAjaxConfirmed(UserEntity userEntity)
        {
            Assignments assignments = new Assignments()
            { ApplicationUserId=userEntity.UserId, AssignDate=DateTime.Now, CourseId=userEntity.returnId, OwnerId= User.Identity.GetUserId() };
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignments);
                db.SaveChanges();
                return RedirectToAction(userEntity.returnMethod, userEntity.returnController, new { id = userEntity.returnId, redirect = userEntity.returnTarget });
            }

            return View(userEntity);
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
        //--------------------------------------------------
        // GET: DeleteAjax/5
        public ActionResult DeleteAjax(UserEntity userEntity)
        {
            var courseId = userEntity.returnId;
            var userId = userEntity.UserId;
            var assignments = db.Assignments.Where(r => r.CourseId == courseId).Where(r => r.ApplicationUserId.Contains(userId))
                .Select(r=> new AssignmentsViewModel { ApplicationUserId= r.ApplicationUserId
                , CourseId=r.CourseId
                , OwnerId=r.OwnerId
                , AssignDate=r.AssignDate }).FirstOrDefault();
            assignments.assignedPropety = userEntity;
            if (assignments == null)
            {
                return RedirectToAction(userEntity.returnMethod, userEntity.returnController, new { id = userEntity.returnId, redirect = userEntity.returnTarget });
            }
            return View("DeleteAjax", assignments);
        }
        //Need to secure
        [HttpPost, ActionName("DeleteAjax")]
        public ActionResult DeleteAjaxConfirmed(UserEntity userEntity)
        {
            Assignments deleteAssigments = db.Assignments
                .Where(r => r.CourseId == userEntity.returnId)
                .Where(r => r.ApplicationUserId.Contains(userEntity.UserId)).FirstOrDefault();
            db.Assignments.Remove(deleteAssigments);
            db.SaveChanges();
            return RedirectToAction(userEntity.returnMethod, userEntity.returnController, new { id = userEntity.returnId, redirect = userEntity.returnTarget  });
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
