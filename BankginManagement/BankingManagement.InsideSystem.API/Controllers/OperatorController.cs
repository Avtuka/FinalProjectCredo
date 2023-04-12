﻿using BankingManagement.Application.Operator;
using BankingManagement.Application.Operator.Requests;
using BankingManagement.Application.Users;
using BankingManagement.Application.Users.Requests;
using BankingManagement.InsideSystem.API.Infrastucture.Auth;
using BankingManagement.InsideSystem.API.Infrastucture.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BankingManagement.InsideSystem.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Operator, CreditOfficer, Administrator")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        #region Private Members and CTOR

        private readonly IUserService _userService;
        private readonly IOperatorService _operatorService;
        private readonly IOptions<JWTConfiguration> _jwtOptions;

        public OperatorController(IUserService userService, IOperatorService operatorService, IOptions<JWTConfiguration> jwtOptions)
        {
            _userService = userService;
            _operatorService = operatorService;
            _jwtOptions = jwtOptions;
        }

        #endregion Private Members and CTOR

        [Route("UserRegistration")]
        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserRegisterRequestModel model, CancellationToken cancellationToken)
        {
            await _userService.RegisterAsync(model, _jwtOptions.Value.Secret, cancellationToken);

            return Ok(ResposneTexts.RegisterUser);
        }

        [Route("LogIn")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> LoginOperator(OperatorLoginModel model, CancellationToken cancellationToken)
        {
            var @operator = await _operatorService.AuthenticateAsync(model, cancellationToken);

            return Ok(JWTHelper.GenerateToken(@operator, _jwtOptions));
        }

        [Route("OperatorRegistration")]
        [HttpPost]
        public async Task<ActionResult> RegisterOperator(OperatorRegisterRequestModel model, CancellationToken cancellationToken)
        {
            await _operatorService.RegisterAsync(model, cancellationToken);

            return Ok(ResposneTexts.RegisterOperator);
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(OperatorChangePasswordModel model, CancellationToken cancellationToken)
        {
            int operatorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _operatorService.ChangePassword(model, operatorId, cancellationToken);

            return Ok();
        }
    }
}