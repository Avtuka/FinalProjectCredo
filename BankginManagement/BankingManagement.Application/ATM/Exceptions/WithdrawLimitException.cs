using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.ATM.Exceptions
{
    public class WithdrawLimitException : Exception
    {
        public readonly string Code = "LimitReached";

        public WithdrawLimitException(string text) : base(text)
        {

        }
    }
}
