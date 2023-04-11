using BankingManagement.Application.Reports.Responses;

namespace BankingManagement.Application.Reports
{
    public interface IReportService
    {
        Task<TransactionReportsResponseModel> GetTransactionReportsMonthlyAsync(int month, CancellationToken cancellationToken);

        Task<UserReportResponseModel> GetUserReportsAsync(CancellationToken cancellationToke);
    }
}