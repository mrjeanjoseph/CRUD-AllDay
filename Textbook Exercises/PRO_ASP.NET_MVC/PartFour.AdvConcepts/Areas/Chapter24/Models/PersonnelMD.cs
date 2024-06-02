using System;
using System.ComponentModel.DataAnnotations;

namespace Chapter24.ModelBinding.Models
{
    [MetadataType(typeof(PersonMetaData))]
    public partial class Person
    {
        public int PersonId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Address HomeAddress { get; set; }
        public bool IsApproved { get; set; }
        public Role Role { get; set; }
        public string Comments { get; set; }

    }

    public enum Role { Admin, User, Contractor, Guest }

    public class Address
    {
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}