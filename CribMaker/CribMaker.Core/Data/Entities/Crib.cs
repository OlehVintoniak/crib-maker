using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Crib : Entity<int>
    {
        [DisplayName("Заголовок")]
        [StringLength(50, ErrorMessage = "Заголовок занадто довгий!")]
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsGlobal { get; set; }
        public virtual Pupil Pupil { get; set; }
    }
}
