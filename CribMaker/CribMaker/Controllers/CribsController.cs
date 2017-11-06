#region

using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using CribMaker.Controllers.Abstract;
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
            return View(cribs);
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