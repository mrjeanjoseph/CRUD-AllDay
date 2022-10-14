using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Main.Models {
    public class Cards {
        public int CardID { get; set; }
        public string CardDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Status { get; set; }
    }
}