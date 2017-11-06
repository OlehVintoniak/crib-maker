#region

using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CribMaker.Controllers.Abstract;
using CribMaker.Models;
using CribMaker.Services.Services;
using CribMaker.Services.Services.Factory;

#endregion

namespace CribMaker.Controllers
{
    public class PupilsController : GeneralController
    {
        private readonly IApplicationUserService _userService;
        private readonly IPupilService _pupilService;
        public PupilsController(IServiceManager serviceManager) : base(serviceManager)
        {
            _userService = serviceManager.ApplicationUserService;
            _pupilService = serviceManager.PupilService;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pupils
        public ActionResult Index()
        {
            var pupils = db.Pupils.Include(p => p.Form).ToList();
            var res = pupils.Select(s => new PupilViewModel(s));
            return View(res);
        }

        // GET: Pupils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupils.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // GET: Pupils/Create
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name");
            return View();
        }

        // POST: Pupils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePupilViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", model.FormId);
                return View(model);
            }

            var pupil = new Pupil
            {
                FormId = model.FormId,
                ApplicationUser = _userService.GetByUserName(model.UserEmail)
            };
            _pupilService.Create(pupil);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CanUserBePupil(string email)
        {
            return Json(_userService.CanBePupil(email));
        }

        // GET: Pupils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupils.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", pupil.FormId);
            return View(pupil);
        }

        // POST: Pupils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pupil pupil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pupil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", pupil.FormId);
            return View(pupil);
        }

        public ActionResult PupilOfTheWeek(int formId)
        {
            var form = db.Forms.FirstOrDefault(f => f.Id == formId);
            var pupilOfTheWeek = db.Pupils.FirstOrDefault(p => p.Id == form.PupilOfTheWeekId);
            return PartialView("_PupilOfTheWeek", pupilOfTheWeek);
        }

        // GET: Pupils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pupil pupil = db.Pupils.Find(id);
            if (pupil == null)
            {
                return HttpNotFound();
            }
            return View(pupil);
        }

        // POST: Pupils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pupil pupil = _pupilService.GetById(id);
            if (pupil == null) return RedirectToAction("Index");

            _pupilService.Remove(pupil);
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