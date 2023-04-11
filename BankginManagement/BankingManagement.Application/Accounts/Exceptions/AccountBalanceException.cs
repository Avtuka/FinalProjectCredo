using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Accounts.Exceptions
{
    public class AccountBalanceException : Exception
    {
        public readonly string Code = "InsufficientBalance";
        public AccountBalanceException(string text) : base(text)
        {

        }
    }
}
