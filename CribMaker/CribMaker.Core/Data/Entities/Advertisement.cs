using System.ComponentModel;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Advertisement: Entity<int>
    {
        [DisplayName("Текст")]
        public string Text { get; set; } 
        [DisplayName("Залоговок")]
        public string Title { get; set; }
        public int PupilId { get; set; }
        public virtual Pupil Pupil { get; set; }
    }
}
