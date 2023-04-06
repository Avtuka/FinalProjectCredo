using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingManagement.Domain.Rates
{
    public class Rate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Quantity { get; set; }
        public double RateFormated { get; set; }
    }
}
