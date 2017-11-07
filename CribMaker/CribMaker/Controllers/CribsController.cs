#region

using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using CribMaker.Controllers.Abstract;
using CribMaker.Core.Consts;
using CribMaker.Models;
using CribMaker.Services.Services.Factory;

#endregion

namespace CribMaker.Controllers
{
    public class CribsController : GeneralController
    {
        public CribsController(IServiceManager serviceManager) : base(serviceManager)
        {

        }
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Cribs
        public ActionResult Index()
        {
            var cribs = _db.Cribs.Where(c => c.IsGlobal).ToList();
            var response = cribs.Select(c => new CribViewModel(c));
            return View(response);
        }

        // GET: Cribs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crib crib = _db.Cribs.Find(id);
            if (crib == null)
            {
                return HttpNotFound();
            }
            return View(crib);
        }

        // GET: Cribs/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Cribs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Crib crib)
        {
            if (ModelState.IsValid)
            {
                crib.PupilId = CurrentUser.Pupil.Id;
                _db.Cribs.Add(crib);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PupilId = new SelectList(_db.Pupils, "Id", "Id", crib.PupilId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", crib.SubjectId);
            return View(crib);
        }

        // GET: Cribs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crib crib = _db.Cribs.Find(id);
            if (crib == null)
            {
                return HttpNotFound();
            }
            ViewBag.PupilId = new SelectList(_db.Pupils, "Id", "Id", crib.PupilId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", crib.SubjectId);
            return View(crib);
        }

        // POST: Cribs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Crib crib)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(crib).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PupilId = new SelectList(_db.Pupils, "Id", "Id", crib.PupilId);
            ViewBag.SubjectId = new SelectList(_db.Subjects, "Id", "Name", crib.SubjectId);
            return View(crib);
        }

        public ActionResult GlobalSearch(string query)
        {
            if (query == string.Empty)
            {
                var cribs = _db.Cribs.ToList();
                var res = cribs.Select(c => new CribViewModel(c));
                return PartialView("_CribsList", res);
            }
            var searchResults = _db.Database
                .SqlQuery<Crib>("SELECT * FROM Cribs WHERE CONTAINS([Text], @queryWithAsterisk)",
                    new SqlParameter("@queryWithAsterisk", $"\"{query}*\""))
                .AsQueryable().Include( c=> c.Subject).Include(c=>c.Pupil).ToList();
            var response = searchResults.Select(sr => new CribViewModel(sr, _db));
            return PartialView("_CribsList", response);
        }



        // GET: Cribs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crib crib = _db.Cribs.Find(id);
            if (crib == null)
            {
                return HttpNotFound();
            }
            return View(crib);
        }

        // POST: Cribs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crib crib = _db.Cribs.Find(id);
            if(crib == null) return RedirectToAction("Index");
            _db.Cribs.Remove(crib);
            _db.SaveChanges();
            return RedirectToAction("Index");
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