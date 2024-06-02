using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chapter24.ModelBinding.Models
{
    [DisplayName("New Person")]
    public partial class PersonMetaData
    {
        //[HiddenInput]
        //[ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }

        public string Title { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }

        [Display(Name ="Approved?")]
        public bool IsApproved { get; set; }

        [UIHint("Enum")]
        public Role Role { get; set; }

        [UIHint("MultilineText")]
        public string Comments { get; set; }

    }
}