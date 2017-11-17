using System.Collections.Generic;
using System.ComponentModel;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Subject : Entity<int>
    {
        [DisplayName("Назва")]
        public string Name { get; set; }

        public virtual ICollection<HomeWork> HomeWorks { get; set; }
        public virtual ICollection<Crib> Cribs { get; set; }

        public Subject()
        {
            HomeWorks = new List<HomeWork>();
            Cribs = new List<Crib>();
        }
    }
}
