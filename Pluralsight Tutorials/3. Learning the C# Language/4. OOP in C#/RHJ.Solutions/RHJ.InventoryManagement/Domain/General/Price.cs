﻿namespace RHJ.InventoryManagement.Domain
{
    public class Price
    {
        public double ItemPrice { get; set; }
        public Currency Currency { get; set; }
        public override string ToString() => $"{ItemPrice} {Currency}";
        
        //public Price(double price, Currency currency)
        //{
        //    ItemPrice = price ;
        //    Currency = currency;
        //}
    }
}
