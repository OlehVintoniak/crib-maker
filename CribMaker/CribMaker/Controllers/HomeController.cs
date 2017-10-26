#region

using CribMaker.Services.Services;
using CribMaker.Services.Services.Factory;
using System.Web.Mvc;

#endregion

namespace CribMaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUserService _userService;

        public HomeController(IServiceManager serviceManager)
        {
            _userService = serviceManager.ApplicationUserService;
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