﻿namespace BankingManagement.Application.Accounts.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public readonly string Code = "AccountNotFound";

        public AccountNotFoundException(string text) : base(text)
        {
        }
    }
}