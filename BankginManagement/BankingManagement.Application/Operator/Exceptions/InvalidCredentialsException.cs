using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Operator.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public readonly string Code = "InvalidCredentials";

        public InvalidCredentialsException(string text) : base(text)
        {

        }
    }
}
