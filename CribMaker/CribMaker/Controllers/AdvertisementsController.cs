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
    public class AdvertisementsController : GeneralController
    {
        public AdvertisementsController(IServiceManager serviceManager) : base(serviceManager)
        {
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Advertisements
        public ActionResult Index()
        {
            var advertisements = db.Advertisements.Include(a => a.Pupil).ToList();
            advertisements.Reverse();
            return View(advertisements);
        }

        // GET: Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Advertisements/Create
        public ActionResult Create()
        {
            ViewBag.PupilId = new SelectList(db.Pupils, "Id", "Id");
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.DateCreated = DateTime.Now;
                advertisement.PupilId = CurrentUser.Pupil.Id;
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PupilId = new SelectList(db.Pupils, "Id", "Id", advertisement.PupilId);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.PupilId = new SelectList(db.Pupils, "Id", "Id", advertisement.PupilId);
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertisement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PupilId = new SelectList(db.Pupils, "Id", "Id", advertisement.PupilId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement != null) 
            db.Advertisements.Remove(advertisement);
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