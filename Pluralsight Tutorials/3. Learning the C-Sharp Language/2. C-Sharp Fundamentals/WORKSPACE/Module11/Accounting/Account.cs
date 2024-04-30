using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingOO
{
    internal class Account
    {
        private string accountNumber;

        public string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                accountNumber = value;
            }
        }

    }
}
