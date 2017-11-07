using System.Linq;
using CribMaker.Core.Data;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Models
{
    public class CribViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsGlobal { get; set; }
        public int PupilId { get; set; }
        public virtual Pupil Pupil { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public CribViewModel(Crib crib, ApplicationDbContext context)
        {
            Title = crib.Title;
            Text = crib.Text;
            IsGlobal = crib.IsGlobal;
            Pupil = context.Cribs.SingleOrDefault(c => c.Id == crib.Id)?.Pupil;
            Subject = context.Cribs.SingleOrDefault(c => c.Id == crib.Id)?.Subject;
        }

        public CribViewModel(Crib crib)
        {
            Title = crib.Title;
            Text = crib.Text;
            IsGlobal = crib.IsGlobal;
            Pupil = crib.Pupil;
            Subject = crib.Subject;
        }
    }
}