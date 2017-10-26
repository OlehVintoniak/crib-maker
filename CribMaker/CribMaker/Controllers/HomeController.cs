using System.Web.Mvc;
using CribMaker.Core.Data;
using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services;
using CribMaker.Services.Services.Factory;

namespace CribMaker.Controllers
{
    public class HomeController : Controller
    {
        private IApplicationUserService _userService;

        public HomeController()
        {
            var context = ApplicationDbContext.Create();
            var uow = new UnitOfWork(context);
            var repositoryManager = new RepositoryManager(context);
            var sm = new ServiceManager(uow,repositoryManager);
            _userService = sm.ApplicationUserService;
        }

        public ActionResult Index()
        {
            var i = _userService.GetByUserName(User.Identity.Name);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}