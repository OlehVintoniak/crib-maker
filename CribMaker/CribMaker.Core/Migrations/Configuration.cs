namespace CribMaker.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CribMaker.Core.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CribMaker.Core.Data.ApplicationDbContext context)
        {
            CreateWithSeedIfNotExist.InitializeDb(context);
        }
    }
}
