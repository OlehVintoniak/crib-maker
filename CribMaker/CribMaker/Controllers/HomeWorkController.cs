#region

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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HomeWork
        public ActionResult Index()
        {
            var homeWorks = db.HomeWorks.Include(h => h.Form).Include(h => h.Subject);
            return View(homeWorks.ToList());
        }

        // GET: HomeWork/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = db.HomeWorks.Find(id);
            if (homeWork == null)
            {
                return HttpNotFound();
            }
            return View(homeWork);
        }

        // GET: HomeWork/Create
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
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
                db.HomeWorks.Add(homeWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", homeWork.SubjectId);
            return View(homeWork);
        }

        // GET: HomeWork/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = db.HomeWorks.Find(id);
            if (homeWork == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", homeWork.SubjectId);
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
                db.Entry(homeWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", homeWork.FormId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", homeWork.SubjectId);
            return View(homeWork);
        }

        // GET: HomeWork/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWork homeWork = db.HomeWorks.Find(id);
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
            HomeWork homeWork = db.HomeWorks.Find(id);
            if(homeWork == null) return RedirectToAction("Index");
            db.HomeWorks.Remove(homeWork);
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