using System.Collections.Generic;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Subject : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<HomeWork> HomeWorks { get; set; }

        public Subject()
        {
            HomeWorks = new List<HomeWork>();
        }
    }
}
