using System.Collections.Generic;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Pupil: Entity<int>
    {
        public int FormId { get; set; }
        public virtual Form Form { get; set; }
        public virtual ICollection<Crib> Cribs { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Pupil()
        {
            Cribs = new List<Crib>();
            Advertisements = new List<Advertisement>();
        }
    }
}
