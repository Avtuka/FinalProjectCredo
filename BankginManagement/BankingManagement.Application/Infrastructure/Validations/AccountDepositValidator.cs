﻿using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class AccountDepositValidator : AbstractValidator<AccountDepositRequestModel>
    {
        public AccountDepositValidator()
        {
            RuleFor(x => x.Amount)
                 .NotEmpty()
                 .GreaterThan(0).WithMessage(ExceptionTexts.DepositAmount);
        }
    }
}