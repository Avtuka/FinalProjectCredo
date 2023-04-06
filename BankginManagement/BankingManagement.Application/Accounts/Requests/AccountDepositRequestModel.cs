using BankingManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Accounts.Requests
{
    public class AccountDepositRequestModel
    {
        public string IBAN { get; set; }
        public double Amount { get; set; }
        public Currencies Currency { get; set; }
    }
}
