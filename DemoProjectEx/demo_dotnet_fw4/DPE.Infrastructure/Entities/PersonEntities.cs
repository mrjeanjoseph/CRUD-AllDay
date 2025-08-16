using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DPE.Infrastructure.Entities
{
    [Table("Person", Schema = "Person")]
    public class PersonEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessEntityID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(8)]
        public string PersonType { get; set; }

        public bool NameStyle { get; set; }

        public bool EmailPromotion { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}