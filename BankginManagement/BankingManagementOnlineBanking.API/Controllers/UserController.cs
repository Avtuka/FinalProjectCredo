using BankingManagement.Application.Accounts;
using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Cards;
using BankingManagement.Application.Users;
using BankingManagement.Application.Users.Requests;
using BankingManagementOnlineBanking.API.Infrastructure.Auth.JWT;
using BankingManagementOnlineBanking.API.Infrastructure.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BankingManagementOnlineBanking.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Bronze, Silver, Gold, Platinum")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private Memebers and CTOR

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;
        private readonly IOptions<JWTConfiguration> _jwtConfiguration;
        private readonly int userId;

        public UserController(IUserService userService, IOptions<JWTConfiguration> configuration, IAccountService accountService, ICardService cardService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _jwtConfiguration = configuration;
            _accountService = accountService;
            _cardService = cardService;

            if (contextAccessor.HttpContext!.User != null && contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
                userId = int.Parse(contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        #endregion Private Memebers and CTOR

        [AllowAnonymous]
        [HttpPost("LogIn")]
        public async Task<ActionResult> LogIn(UserLoginRequestModel model, CancellationToken cancellationToken)
        {
            var user = await _userService.Authenticate(model, cancellationToken);

            return Ok(JWTHelper.GenerateToken(user, _jwtConfiguration));
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(UserPasswordChangeModel model, CancellationToken cancellationToken)
        {
            await _userService.ChangePassword(model, userId, cancellationToken);

            return Ok();
        }

        [HttpGet("MyAccounts")]
        public async Task<ActionResult> GetAccounts(CancellationToken cancellationToken)
        {
            var accounts = await _accountService.GetAccounts(userId, cancellationToken);

            return Ok(accounts);
        }

        [HttpGet("MyCards")]
        public async Task<ActionResult> GetCards(CancellationToken cancellationToken)
        {
            var cards = await _cardService.GetCardsAsymc(userId, cancellationToken);

            return Ok(cards);
        }

        [HttpPut("TransferToMyAccount")]
        public async Task<ActionResult> TransferToOwnAccount(TransferModelRequest model, CancellationToken cancellationToken)
        {
            await _accountService.TransferToOwnAccountAsync(model, userId, cancellationToken);

            return Ok(ResponseTexts.Transaction);
        }

        [Route("Transfer")]
        [HttpPut]
        public async Task<ActionResult> TransferToAnotherAccount(TransferModelRequest model, CancellationToken cancellationToken)
        {
            await _accountService.TransferToOtherAccountAsync(model, userId, cancellationToken);

            return Ok(ResponseTexts.Transaction);
        }

        [HttpPut]
        public async Task<ActionResult> Deposit(AccountDepositRequestModel model, CancellationToken cancellationToken)
        {
            await _accountService.DepositAsync(model, userId, cancellationToken);

            return Ok(ResponseTexts.Deposit);
        }

        [Route("ConfirmEmail")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string code, CancellationToken cancellationToken = default)
        {
            await _userService.ConfirmEmailAsync(code, _jwtConfiguration.Value.Secret, cancellationToken);

            return Ok("Email Confirmed");
        }
    }
}