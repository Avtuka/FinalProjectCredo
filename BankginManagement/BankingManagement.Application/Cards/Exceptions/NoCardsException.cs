using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Cards.Exceptions
{
    public class NoCardsException : Exception
    {
        public readonly string Code = "NoCards";

        public NoCardsException(string text) : base(text)
        {

        }
    }
}
