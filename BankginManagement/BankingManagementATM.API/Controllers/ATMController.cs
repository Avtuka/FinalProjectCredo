using BankingManagement.Application.ATM;
using BankingManagement.Application.ATM.Requests;
using BankingManagement.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementATM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        #region Private Members and CTOR
        private readonly IATMService _service;

        public ATMController(IATMService service)
        {
            _service = service;
        }
        #endregion

        [HttpGet("Balance")]
        public async Task<ActionResult> GetBalance(AuthenticateCardRequestModel cardModel, CancellationToken cancellationToken)
        {
            var result = await _service.SeeBalanceAsync(cardModel, cancellationToken);

            return Ok(result);
        }

        [HttpPut("Pin")]
        public async Task<ActionResult> ChangePin(AuthenticateCardRequestModel cardModel, ChangePinRequestModel pinModel, CancellationToken cancellationToken)
        {
            await _service.ChangePin(cardModel, pinModel, cancellationToken);

            return Ok("Pin was changed");
        }

        [HttpPut]
        public async Task<ActionResult> WithdrawMoeny(AuthenticateCardRequestModel cardModel, Currencies currency, double amount, CancellationToken cancellationToken)
        {
            await _service.WithdrawAsync(cardModel, currency, amount, cancellationToken);

            return Ok("Withdraw Succesfull");
        }

    }
}
