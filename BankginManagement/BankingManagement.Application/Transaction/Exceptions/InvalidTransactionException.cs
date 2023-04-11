using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Transaction.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public readonly string Code = "InvalidTransaction";

        public InvalidTransactionException(string text) : base(text)
        {

        }
    }
}
