using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Users.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public readonly string Code = "EmailNotConfrimed";

        public EmailNotConfirmedException(string text) : base(text)
        {

        }
    }
}
