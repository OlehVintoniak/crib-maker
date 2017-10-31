using System.Data.Entity.ModelConfiguration;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Core.Data.Configurations
{
    public class FormConfiguration:EntityTypeConfiguration<Form>
    {
        public FormConfiguration()
        {
            ToTable("Forms");
            HasKey(k => k.Id);
        }
    }
}
