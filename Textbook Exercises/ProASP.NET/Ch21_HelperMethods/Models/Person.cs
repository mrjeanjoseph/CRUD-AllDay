using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ch21_HelperMethods.Models {

    [DisplayName("New Person")]
    public class Person {

        [HiddenInput(DisplayValue =false)]
        public int PersonId { get; set; }

        [Display(Name="First"), UIHint("MultiLineText")]
        public string FirstName { get; set; }

        [Display(Name = "Last")]
        public string LastName { get; set; }

        [Display(Name = "BirthDate"), DataType(DataType.Date)]        
        public DateTime BirthDate { get; set; }

        public Address HomeAddress { get; set; }

        [Display(Name = "Approved")]
        public bool IsApprove { get; set; }
        public Role Role { get; set; }
    }

    public enum Role {
        Admin,User,Guest
    }
}