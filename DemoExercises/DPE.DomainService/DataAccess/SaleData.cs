﻿using DPE.DomainService.Models;
using DPE.DomainService.UserData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DPE.DomainService.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData _product;
        private readonly ISqlDataAccess _sqlData;

        public SaleData(IProductData product, ISqlDataAccess sqlData)
        {
            _product = product;
            _sqlData = sqlData;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            // TODO: Make this SOLID/DRY/Better
            // Start filling in the detail model models we will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = ConfigHelper.GetTaxRate();

            // Fill in the available information
            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };

                //Get the information about this product
                var productInfo = _product.GetProductById(item.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {item.ProductId} could not be found in the database");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }
                details.Add(detail);
            }

            // Create the sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;


            try
            {
                _sqlData.StartTransaction("CCMSConn");

                // Save the sale model
                _sqlData.SaveDataForTransaction("dbo.spSaleInsert", sale);

                // Get the Id from the sale models
                sale.Id = _sqlData.LoadDataForTransaction<int, dynamic>("spSaleLookup",
                    new { cashierId = sale.CashierId, sale.SaleDate }).FirstOrDefault();

                // Fininsh filling in the sale detail models
                foreach (var item in details)
                {
                    item.SaleId = sale.Id;

                    // Save the sale detail models
                    _sqlData.SaveDataForTransaction("dbo.spSaleDetailInsert", item);
                }

                _sqlData.CommitTransaction();
            }
            catch
            {
                _sqlData.RollBackTransaction();
                throw;
            }

        }

        public List<SaleReportModel> GetSaleReport()
        {
            return _sqlData.LoadData<SaleReportModel, dynamic>("dbo.spSaleSalesReport", new { }, "CCMSConn");
        }
    }
}
