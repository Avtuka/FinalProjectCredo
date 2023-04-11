using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Operator.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public readonly string Code = "DuplicateEmail";

        public DuplicateEmailException(string text) : base(text)
        {

        }
    }
}
