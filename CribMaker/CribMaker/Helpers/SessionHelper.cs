#region

using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;
using System.Linq;
using System.Web;

#endregion

namespace CribMaker.Helpers
{
    public class SessionHelper
    {
        public static string GetCurrentUserNameLastName()
        {
            using (var context = ApplicationDbContext.Create())
            {
                return $"{CurrentUser(context).FirstName} {CurrentUser(context).LastName}";
            }
        }

        public static bool IsUserInForm()
        {
            using (var context = ApplicationDbContext.Create())
            {
                var user = CurrentUser(context);
                return context.Users.FirstOrDefault(u => u.UserName == user.UserName)?.Pupil != null;
            }
        }

        public static Form CurrentForm()
        {
            using (var context = ApplicationDbContext.Create())
            {
                return CurrentUser(context).Pupil.Form;
            }
        }

        #region Helpers

        private static ApplicationUser CurrentUser(ApplicationDbContext context)
        {
            return context.Users.FirstOrDefault(u => u.UserName == HttpContext.Current.User.Identity.Name);
        }

        #endregion
    }
}