using CribMaker.Core.Data;
using CribMaker.Core.Repositories.Abstract;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Factory;
using System.Web;

namespace CribMaker.Helpers
{
    public class SessionHelper
    {
        public static string GetCurrentUserNameLastName()
        {
            var context = ApplicationDbContext.Create();
            var serviceManager = new ServiceManager(new UnitOfWork(context),new RepositoryManager(context));
            var userName = HttpContext.Current.User.Identity.Name;
            var appUser = serviceManager.ApplicationUserService.GetByUserName(userName);
            return $"{appUser.FirstName} {appUser.LastName}";
        }
    }
}