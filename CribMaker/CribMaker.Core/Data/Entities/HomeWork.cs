using CribMaker.Core.Data.Entities.Abstract;
using System;
using System.ComponentModel;

namespace CribMaker.Core.Data.Entities
{
    public class HomeWork : Entity<int>
    {
        [DisplayName("Домашнє завдання")]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int FormId { get; set; }

        public virtual Form Form { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject{ get; set; }
    }
}
