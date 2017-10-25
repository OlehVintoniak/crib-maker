#region

using CribMaker.Core.Data;
using System.Data.Entity;
using System.Linq;
using CribMaker.Core.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

#endregion

namespace CribMaker.Core.Migrations
{
    public class CreateWithSeedIfNotExist : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPasswrod = "1z1z1zZ_";

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeDb(context);
        }

        public static void InitializeDb(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == Consts.Consts.AdminRole))
            {
                Init(context);
            }
        }
        private static void Init(ApplicationDbContext context)
        {
            InitUsers(context);
        }
        private static void InitUsers(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole { Name = Consts.Consts.AdminRole });
            roleManager.Create(new IdentityRole { Name = Consts.Consts.UserRole });

            var userHelper = new UserHelper(context);

            var admin = userHelper.GetUser(AdminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    Email = AdminEmail,
                    UserName = AdminEmail,
                    EmailConfirmed = true
                };
                userManager.Create(admin, AdminPasswrod);
                context.SaveChanges();
            }
        }

        #region Helpers

        private class UserHelper
        {
            public UserHelper(ApplicationDbContext context)
            {
                Context = context;
            }

            private ApplicationDbContext Context { get; set; }

            public ApplicationUser GetUser(string email)
            {
                return Context.Users.FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        #endregion
    }
}