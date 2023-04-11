using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Users.Exceptions
{
    public class MoreThanOneAccountException : Exception
    {
        public readonly string Code = "CardCannotHaveDuplicateCurrencyAccount";
        public MoreThanOneAccountException(string text) : base(text)
        {

        }
    }
}
