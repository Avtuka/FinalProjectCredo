namespace BankingManagement.Application.Reports.Responses
{
    public class TransactionReportsResponseModel
    {
        public int TransactionCount { get; set; }
        public double IncomeGel { get; set; }
        public double IncomeUsd { get; set; }
        public double IncomeEur { get; set; }
        public double AverageIncomeGel { get; set; }
        public double AverageIncomeUsd { get; set; }
        public double AverageIncomeEur { get; set; }
        public double ATMWithdrawGel { get; set; }
        public double ATMWithdrawUsd { get; set; }
        public double ATMWithdrawEur { get; set; }
    }
}