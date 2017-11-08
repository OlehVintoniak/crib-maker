using System.Data.Entity.ModelConfiguration;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Core.Data.Configurations
{
    public class PupilConfiguration: EntityTypeConfiguration<Pupil>
    {
        public PupilConfiguration()
        {
            ToTable("Pupils");
            HasKey(k => k.Id);

            HasRequired(u => u.ApplicationUser)
                .WithOptional(p => p.Pupil)
                .WillCascadeOnDelete(false);

            HasRequired(f => f.Form)
                .WithMany(p => p.Pupils)
                .WillCascadeOnDelete(false);

            HasMany(p=>p.Advertisements)
                .WithRequired(a=>a.Pupil)
                .WillCascadeOnDelete(false);
        }
    }
}
