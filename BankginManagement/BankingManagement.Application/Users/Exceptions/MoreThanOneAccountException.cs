﻿namespace BankingManagement.Application.Users.Exceptions
{
    public class MoreThanOneAccountException : Exception
    {
        public readonly string Code = "CardCannotHaveDuplicateCurrencyAccount";

        public MoreThanOneAccountException(string text) : base(text)
        {
        }
    }
}