using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain
{
    public class ShippingDetails
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string FullLegalName { get; set; }

        [Required(ErrorMessage = "Please enter name and street address")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter state")]
        public string State { get; set; }

        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}