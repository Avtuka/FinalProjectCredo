using BankingManagement.Application.ATM;
using BankingManagement.Application.ATM.Requests;
using BankingManagement.Domain.Enums;
using BankingManagementATM.API.Infrastucture.Resources;
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

        #endregion Private Members and CTOR

        [HttpGet("Balance")]
        public async Task<ActionResult> GetBalance([FromQuery] AuthenticateCardRequestModel cardModel, CancellationToken cancellationToken)
        {
            var result = await _service.SeeBalanceAsync(cardModel, cancellationToken);

            return Ok(result);
        }

        [HttpPut("Pin")]
        public async Task<ActionResult> ChangePin([FromQuery] AuthenticateCardRequestModel cardModel, [FromBody] ChangePinRequestModel pinModel, CancellationToken cancellationToken)
        {
            await _service.ChangePin(cardModel, pinModel, cancellationToken);

            return Ok(ResponseTexts.PINChange);
        }

        [HttpPut]
        public async Task<ActionResult> WithdrawMoeny(AuthenticateCardRequestModel cardModel, Currencies currency, double amount, CancellationToken cancellationToken)
        {
            await _service.WithdrawAsync(cardModel, currency, amount, cancellationToken);

            return Ok(ResponseTexts.Withdraw);
        }
    }
}