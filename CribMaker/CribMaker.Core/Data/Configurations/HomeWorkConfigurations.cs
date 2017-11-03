using System.Data.Entity.ModelConfiguration;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Core.Data.Configurations
{
    public class HomeWorkConfigurations:EntityTypeConfiguration<HomeWork>
    {
        public HomeWorkConfigurations()
        {
            ToTable("HomeWorks");
            HasKey(k => k.Id);

            HasRequired(h => h.Form)
                .WithMany(f => f.HomeWorks)
                .WillCascadeOnDelete(false);

            HasRequired(h => h.Subject)
                .WithMany(s => s.HomeWorks)
                .WillCascadeOnDelete(false);
        }
    }
}
