using System;
using System.Configuration;

namespace CCMS.DomainService
{
    public class ConfigHelper
    {
        // TODO: Move this from config to the API
        public static decimal GetTaxRate()
        {
            string rateText = ConfigurationManager.AppSettings["taxRate"];

            bool isValidTaxRate = Decimal.TryParse(rateText, out decimal result);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("Tax rate is not properly setup");
            }

            return result / 100;
        }
    }
}
