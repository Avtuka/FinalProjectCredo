using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Accounts.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public readonly string Code = "AccountNotFound";

        public AccountNotFoundException(string text) : base(text)
        {

        }
    }
}
