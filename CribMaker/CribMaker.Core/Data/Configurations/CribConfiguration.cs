using System.Data.Entity.ModelConfiguration;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Core.Data.Configurations
{
    public class CribConfiguration: EntityTypeConfiguration<Crib>
    {
        public CribConfiguration()
        {
            ToTable("Cribs");
            HasKey(k => k.Id);

            HasRequired(p => p.Pupil)
                .WithMany(c => c.Cribs);
        }
    }
}
