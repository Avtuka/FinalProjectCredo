using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidPINException : Exception
    {
        public readonly string Code = "InvalidPin";
        public InvalidPINException(string text) : base(text)
        {

        }
    }
}
