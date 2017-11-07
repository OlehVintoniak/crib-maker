using System.Data.Entity;
using CribMaker.Core.Data;

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
            CreateFullTextIndex(context, "Cribs", "Text", "Id");
        }

        private void CreateFullTextIndex(ApplicationDbContext context, string table, string column, string key)
        {
            var selectUniqueIndex = $"SELECT * FROM sysindexes WHERE id=object_id('{table}') and name='IX_{key}'";
            var createFullTextCatalog = $"CREATE FULLTEXT CATALOG FTXC_{table} AS DEFAULT;";
            var createUniqueIndex = $"CREATE UNIQUE INDEX IX_{key} ON {table} ({key});";
            var createFullTextIndex = $"CREATE FULLTEXT INDEX ON {table}({column} LANGUAGE 0x0) KEY INDEX [IX_{key}] ON ([FTXC_{table}]) WITH STOPLIST = SYSTEM;";
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                $"IF NOT EXISTS ({selectUniqueIndex}) BEGIN {createUniqueIndex} {createFullTextCatalog} {createFullTextIndex} END");
        }
    }
}
