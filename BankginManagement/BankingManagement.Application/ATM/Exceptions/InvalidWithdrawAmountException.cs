using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidWithdrawAmountException : Exception
    {
        public readonly string Code = "InvalidAmount";

        public InvalidWithdrawAmountException(string text) : base(text)
        {

        }
    }
}
