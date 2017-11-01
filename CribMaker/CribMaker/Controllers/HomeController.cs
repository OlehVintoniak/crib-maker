#region

using System.Web.Mvc;
using CribMaker.Services.Services;
using CribMaker.Controllers.Abstract;
using CribMaker.Services.Services.Factory;

#endregion

namespace CribMaker.Controllers
{
    public class HomeController : GeneralController
    {
        private readonly IApplicationUserService _userService;

        public HomeController(IServiceManager serviceManager) : base(serviceManager)
        {
            _userService = serviceManager.ApplicationUserService;
        }

        public ActionResult Index()
        {
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