#region

using System;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CribMaker.Controllers.Abstract;
using CribMaker.Services.Services.Factory;

#endregion

namespace CribMaker.Controllers
{
    public class HomeWorkController : GeneralController
    {
        public HomeWorkController(IServiceManager serviceManager) : base(serviceManager)
        {
        }
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: HomeWork
        public ActionResult Index()
        {
            var homeWorks = _db.HomeWorks.Include(h => h.Form).Include(h => h.Subject);
            return View(homeWorks.ToList());
        }

        // GET: HomeWork/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = _db.HomeWorks.Find(id);
            if (homeWork == null)
            {
                return HttpNotFound();
            }
            return View(homeWork);
        }

        // GET: HomeWork/Create
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(_db.Forms, "Id", "Name");
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name");
            return View();
        }

        // POST: HomeWork/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HomeWork homeWork)
        {
            if (ModelState.IsValid)
            {
                _db.HomeWorks.Add(homeWork);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormId = new SelectList(_db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", homeWork.SubjectId);
            return View(homeWork);
        }

        // GET: HomeWork/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = _db.HomeWorks.Find(id);
            if (homeWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(_db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", homeWork.SubjectId);
            return View(homeWork);
        }

        // POST: HomeWork/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HomeWork homeWork)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(homeWork).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(_db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", homeWork.SubjectId);
            return View(homeWork);
        }

        // GET: HomeWork/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = _db.HomeWorks.Find(id);
            if (homeWork == null)
            {
                return HttpNotFound();
            }
            return View(homeWork);
        }

        // POST: HomeWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeWork homeWork = _db.HomeWorks.Find(id);
            if(homeWork == null) return RedirectToAction("Index");
            _db.HomeWorks.Remove(homeWork);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult HomeWorksBySubject(int subjectId, int formId)
        {
            var homeWorks = _db.HomeWorks
                .Where(h => h.FormId == formId && h.SubjectId == subjectId)
                .ToList();
            return PartialView("_HomeWorkList", homeWorks);
        }

        public ActionResult HomewHorksOnTomorow(int formId)
        {
            var homeWorks = _db.HomeWorks
                .Where(h => h.Date.Day == DateTime.Now.Day + 1
                    && h.Date.Month == DateTime.Now.Month
                    && h.Date.Year == DateTime.Now.Year
                    && h.FormId == formId)
                .ToList();
            return PartialView("_HomeWorkList", homeWorks);
        }

        public ActionResult HomewHorksOnTomorowNotification()
        {
            var homeWorks = _db.HomeWorks
                .Where(h => h.Date.Day == DateTime.Now.Day + 1
                            && h.Date.Month == DateTime.Now.Month
                            && h.Date.Year == DateTime.Now.Year
                            && h.FormId == CurrentUser.Pupil.FormId)
                .ToList();
            return PartialView("_ForTomorowNotificationList", homeWorks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}