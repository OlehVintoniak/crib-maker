using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CribMaker.Core.Data.Entities.Abstract;

namespace CribMaker.Core.Data.Entities
{
    public class Advertisement: Entity<int>
    {
        [DisplayName("Текст")]
        public string Text { get; set; } 
        [DisplayName("Залоговок")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public int PupilId { get; set; }
        public virtual Pupil Pupil { get; set; }
    }
}
