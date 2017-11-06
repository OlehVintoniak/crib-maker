using System.Collections.Generic;
using System.ComponentModel;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Form : Entity<int>
    {
        [DisplayName("Клас")]
        public string Name { get; set; }

        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<HomeWork> HomeWorks { get; set; }
        public int? PupilOfTheWeekId { get; set; }

        public Form()
        {
            Pupils = new List<Pupil>();
            HomeWorks = new List<HomeWork>();
        }
    }
}
