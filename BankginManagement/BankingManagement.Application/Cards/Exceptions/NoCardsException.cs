﻿namespace BankingManagement.Application.Cards.Exceptions
{
    public class NoCardsException : Exception
    {
        public readonly string Code = "NoCards";

        public NoCardsException(string text) : base(text)
        {
        }
    }
}