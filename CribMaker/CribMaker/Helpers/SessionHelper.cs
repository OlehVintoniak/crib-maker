#region

using System.Web;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using CribMaker.Core.Repositories.Factory;
using CribMaker.Services.Services.Factory;
using CribMaker.Core.Repositories.Abstract;

#endregion

namespace CribMaker.Helpers
{
    public class SessionHelper
    {
        private static readonly ApplicationDbContext Context = ApplicationDbContext.Create();
        private static readonly ServiceManager ServiceManager = new ServiceManager(new UnitOfWork(Context), new RepositoryManager(Context));

        private static ApplicationUser AppUser =>
             ServiceManager.ApplicationUserService.GetByUserName(HttpContext.Current.User.Identity.Name);
        public static string GetCurrentUserNameLastName()
        {
            return $"{AppUser.FirstName} {AppUser.LastName}";
        }

        public static bool IsUserInForm()
        {
            return AppUser.PupilId == null;
        }
    }
}