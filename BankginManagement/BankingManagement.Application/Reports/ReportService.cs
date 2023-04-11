using BankingManagement.Application.Reports.Responses;
using BankingManagement.Application.Repositories;
using BankingManagement.Domain.User;

namespace BankingManagement.Application.Reports
{
    internal class ReportService : IReportService
    {
        #region Private Members and CTOR

        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Domain.Transactions.Transaction> _transactionRepo;

        public ReportService(IRepository<User> userRepo, IRepository<Domain.Transactions.Transaction> transactionRepo)
        {
            _userRepo = userRepo;
            _transactionRepo = transactionRepo;
        }

        #endregion Private Members and CTOR

        public async Task<TransactionReportsResponseModel> GetTransactionReportsMonthlyAsync(int month, CancellationToken cancellationToken)
        {
            if (month < 0)
                throw new Exception("Invalid filter for month");

            var report = new TransactionReportsResponseModel();

            var transactions = new List<Domain.Transactions.Transaction>();

            if (month == 0)
                transactions = await _transactionRepo.GetAllAsync(cancellationToken: cancellationToken);
            else
                transactions = await _transactionRepo.GetAllByPredicateAsync(x => x.Date.AddMonths(-month) <= DateTime.UtcNow, cancellationToken);

            if (transactions.Count() == 0)
                return report;

            report.TransactionCount = transactions.Count();
            report.IncomeGel = transactions.Where(x => x.Currency == Domain.Enums.Currencies.GEL).Sum(x => x.Comission);
            report.IncomeUsd = transactions.Where(x => x.Currency == Domain.Enums.Currencies.USD).Sum(x => x.Comission);
            report.IncomeEur = transactions.Where(x => x.Currency == Domain.Enums.Currencies.EUR).Sum(x => x.Comission);

            report.AverageIncomeGel = transactions.Where(x => x.Currency == Domain.Enums.Currencies.GEL).Average(x => x.Comission);
            report.AverageIncomeUsd = transactions.Where(x => x.Currency == Domain.Enums.Currencies.USD).Average(x => x.Comission);
            report.AverageIncomeEur = transactions.Where(x => x.Currency == Domain.Enums.Currencies.EUR).Average(x => x.Comission);

            report.ATMWithdrawGel = transactions.Where(x => x.TransactionType == Domain.Enums.TransactionTypes.Withdraw && x.Currency == Domain.Enums.Currencies.GEL).Sum(x => x.Amount);
            report.ATMWithdrawUsd = transactions.Where(x => x.TransactionType == Domain.Enums.TransactionTypes.Withdraw && x.Currency == Domain.Enums.Currencies.USD).Sum(x => x.Amount);
            report.ATMWithdrawEur = transactions.Where(x => x.TransactionType == Domain.Enums.TransactionTypes.Withdraw && x.Currency == Domain.Enums.Currencies.EUR).Sum(x => x.Amount);

            return report;
        }

        public async Task<UserReportResponseModel> GetUserReportsAsync(CancellationToken cancellationToken)
        {
            var report = new UserReportResponseModel();
            report.NewUsersInThisYear = (await _userRepo.GetAllByPredicateAsync(x => x.CreatedOn.Year == DateTime.UtcNow.Year, cancellationToken)).Count();
            report.NewUsersDuringLastYear = (await _userRepo.GetAllByPredicateAsync(x => x.CreatedOn.AddYears(-1) >= DateTime.UtcNow, cancellationToken)).Count();
            report.NewUsersInLastMonth = (await _userRepo.GetAllByPredicateAsync(x => x.CreatedOn.AddDays(-30) >= DateTime.UtcNow, cancellationToken)).Count();

            return report;
        }
    }
}