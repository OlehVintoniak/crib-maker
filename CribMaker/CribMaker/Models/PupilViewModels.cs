using System.ComponentModel;
using CribMaker.Core.Data.Entities;

namespace CribMaker.Models
{
    public class CreatePupilViewModel
    {
        [DisplayName("Клас")]
        public int FormId { get; set; }

        [DisplayName("Email учня")]
        public string UserEmail { get; set; }
    }

    public class PupilViewModel
    {
        public int  Id { get; set; }
        public string FormName { get; set; }
        public string FullName { get; set; }

        public PupilViewModel(Pupil pupil)
        {
            Id = pupil.Id;
            FormName = pupil.Form.Name;
            FullName = pupil.ApplicationUser.FirstName + pupil.ApplicationUser.LastName;
        }
    }
}