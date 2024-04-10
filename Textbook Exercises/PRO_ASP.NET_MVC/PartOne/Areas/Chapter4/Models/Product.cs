namespace EssentialFeatures.Models {

    public class Product {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public string Category { get; set; }
    }

    public class RegularlyDefiningProperty {
        public int ProductID { get; set; }

        private string productName;
        public string ProductName {
            get { return ProductID + productName; }
            set { productName = value; }
        }


        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public string Category { get; set; }
    }

    public class DefiningProperty_StillOldWays {
        private int ProductId;

        private string _name;
        private string _description;
        private decimal _price;
        private string _category;

        public int ProductID {
            get { return ProductId; }
            set { ProductId = value; }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string Description {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Price {
            get { return _price; }
            set { _price = value; }
        }

        public string Category {
            get { return _category; }
            set { _category = value; }
        }
    }    
    
    public class DefiningProperty_OldWays {
        private string name;
        public string Name { get { return name; } set { name = value; } }
    }
}