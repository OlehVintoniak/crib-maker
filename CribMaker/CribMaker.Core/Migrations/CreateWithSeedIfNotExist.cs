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

        private static readonly string[][] DefaultUsersNames =
        {
            new []{"Name1","Surname1"},
            new []{"Name2", "Surname2"},
            new []{"Name3", "Surname3"}
        };
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

        #region Users

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
                    FirstName = "admin",
                    LastName = "adminuch",
                    EmailConfirmed = true
                };
                var result = userManager.Create(admin, AdminPasswrod);

                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, Consts.Consts.AdminRole);
                    userManager.AddToRole(admin.Id, Consts.Consts.UserRole);
                }
                context.SaveChanges();
            }

            foreach (var userN in DefaultUsersNames)
            {
                var email = userN[1] + "@gmail.com";
                var existedUser = userHelper.GetUser(email);
                if (existedUser != null)continue;

                var user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = userN[0],
                    LastName = userN[1],
                    EmailConfirmed = true
                };
                var result = userManager.Create(user, AdminPasswrod);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, Consts.Consts.UserRole);
                }
                context.SaveChanges();
            }
        }

        #endregion

        private static void InitForms(ApplicationDbContext context)
        {
            
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