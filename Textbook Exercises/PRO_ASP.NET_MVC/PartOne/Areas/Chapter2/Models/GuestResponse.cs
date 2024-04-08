using System.ComponentModel.DataAnnotations;

namespace Chapter2.PartyInvites {
    public class GuestResponse {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter your name")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Please specify if you will attend")]
        public bool? WillAttend { get; set; }
    }
}