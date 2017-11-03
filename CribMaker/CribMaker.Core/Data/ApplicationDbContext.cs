#region

using CribMaker.Core.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CribMaker.Core.Data.Configurations;

#endregion

namespace CribMaker.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Crib> Cribs { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Subject> Subjects { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfigurations());
            modelBuilder.Configurations.Add(new FormConfiguration());
            modelBuilder.Configurations.Add(new CribConfiguration());
            modelBuilder.Configurations.Add(new PupilConfiguration());
            modelBuilder.Configurations.Add(new HomeWorkConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}