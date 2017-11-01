using System.Data.Entity.ModelConfiguration;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Core.Data.Configurations
{
    public class ApplicationUserConfigurations: EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfigurations()
        {
            ToTable("AspNetUsers");
            HasKey(k => k.Id);
        }
    }
}
