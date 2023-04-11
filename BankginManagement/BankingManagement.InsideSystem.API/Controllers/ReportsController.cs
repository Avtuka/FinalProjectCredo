using BankingManagement.Application.Reports;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagement.InsideSystem.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "CreditOfficer, Administrator")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region Private Members and CTOR

        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        #endregion Private Members and CTOR

        [HttpGet("Users")]
        public async Task<ActionResult> GetUserReport(CancellationToken cancellationToken)
        {
            var report = await _service.GetUserReportsAsync(cancellationToken);

            return Ok(report);
        }

        [HttpGet("Transactions")]
        public async Task<ActionResult> GetTransactionReportsMonth(int month, CancellationToken cancellationToken)
        {
            var report = await _service.GetTransactionReportsMonthlyAsync(month, cancellationToken);

            return Ok(report);
        }
    }
}