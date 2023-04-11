using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Users.Exceptions
{
    public class EmailAlreadyConfirmedException : Exception
    {
        public readonly string Code = "EmailAlreadyConfirmed";

        public EmailAlreadyConfirmedException(string text) : base(text)
        {

        }
    }
}
