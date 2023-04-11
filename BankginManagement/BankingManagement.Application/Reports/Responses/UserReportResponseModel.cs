namespace BankingManagement.Application.Reports.Responses
{
    public class UserReportResponseModel
    {
        public int NewUsersInThisYear { get; set; }
        public int NewUsersDuringLastYear { get; set; }
        public int NewUsersInLastMonth { get; set; }
    }
}