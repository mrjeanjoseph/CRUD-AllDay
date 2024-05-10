using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain
{
    public class Merchandise
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public string Category {  get; set; }
        public decimal Price { get; set; }
        public bool IsValid { get; set; }
    }
}