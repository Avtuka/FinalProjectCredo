using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Transaction.Exceptions
{
    public class EmptyTransactionException : Exception
    {
        public readonly string Code = "EmptyTransaction";

        public EmptyTransactionException(string text) : base(text)
        {

        }
    }
}
