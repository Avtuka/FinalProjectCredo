using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidCurrencyException : Exception
    {
        public readonly string Code = "InvalidCurrency";

        public InvalidCurrencyException(string text) : base(text)
        {

        }
    }
}
