using BankingManagement.Application.Accounts;
using BankingManagement.Application.Cards;
using BankingManagement.Application.Infrastructure.Helpers;
using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Operator.Exceptions;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Users.Exceptions;
using BankingManagement.Application.Users.Requests;
using BankingManagement.Domain.Card;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.User;
using IbanNet.Registry;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingManagement.Application.Users
{
    internal class UserService : IUserService
    {
        #region Private Members and CTOR

        private readonly IRepository<User> _repo;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;

        public UserService(IRepository<User> repo, IAccountService accountService, ICardService cardService)
        {
            _repo = repo;
            _accountService = accountService;
            _cardService = cardService;
        }

        #endregion Private Members and CTOR

        public async Task<User> Authenticate(UserLoginRequestModel model, CancellationToken cancellationToken)
        {
            var passwordHash = MyPasswordHelper.GenerateSHA512Hash(model.Password);
            var user = await _repo.GetByPredicateAsync(x => x.Email == model.Email && x.PasswordHash == passwordHash, cancellationToken);
            if (user == null)
                throw new InvalidCredentialsException(ExceptionTexts.InvalidCredentials);
            if (!user.EmailConfirmed)
                throw new EmailNotConfirmedException(ExceptionTexts.EmailNotConfirmed);

            return user;
        }

        public async Task ChangePassword(UserPasswordChangeModel model, int userId, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByPredicateAsync(x => x.Id == userId && x.PasswordHash == MyPasswordHelper.GenerateSHA512Hash(model.CurrentPassword), cancellationToken);

            if (user == null)
                throw new InvalidCredentialsException(ExceptionTexts.InvalidCredentials);

            user.PasswordHash = MyPasswordHelper.GenerateSHA512Hash(model.NewPassword);

            _repo.Update(user);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task ConfirmEmailAsync(string code, string secret, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = "EmailConfirmation",
                ValidateAudience = true,
                ValidAudience = "User",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(10)
            };

            SecurityToken validatedToken;
            var claimsPrincipal = tokenHandler.ValidateToken(code, validationParameters, out validatedToken);

            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
                throw new Exception("Something went wrong try again");

            var user = await _repo.GetByPredicateAsync(x => x.Email == email, cancellationToken);

            if (user.EmailConfirmed)
                throw new EmailAlreadyConfirmedException(ExceptionTexts.EmailAlreadyConfirmed);

            user.EmailConfirmed = true;

            _repo.Update(user);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task RegisterAsync(UserRegisterRequestModel model, string secret, CancellationToken cancellationToken)
        {
            var userToRegister = model.Adapt<User>();

            var password = MyPasswordHelper.GenerateRandomPassword();

            userToRegister.PasswordHash = MyPasswordHelper.GenerateSHA512Hash(password);

            int gelCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.GEL).Count();
            if (gelCount > 1)
                throw new MoreThanOneAccountException(ExceptionTexts.DuplicateGELAccount);

            int usdCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.USD).Count();
            if (gelCount > 1)
                throw new MoreThanOneAccountException(ExceptionTexts.DuplicateUSDAccount);

            int eurCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.EUR).Count();
            if (gelCount > 1)
                throw new MoreThanOneAccountException(ExceptionTexts.DuplicateEURAccount);

            //Card Generation
            Card card;
            do
            {
                card = GenerateNewCard(userToRegister.FirstName + " " + userToRegister.LastName);
            } while (await _cardService.CardNumberExistsAsync(card.CardNumber, cancellationToken));

            //Assign card to accounts
            foreach (var acc in userToRegister.Accounts)
            {
                acc.Card = card;
            }

            //IBAN Generation
            var ibnGen = new IbanGenerator();
            string iban;
            do
            {
                iban = ibnGen.Generate("GE").ToString();
            } while (await _accountService.IbanExistsAsync(iban, cancellationToken));

            foreach (var acc in userToRegister.Accounts)
            {
                acc.IBAN = iban + acc.Currency.ToString();
            }

            await _repo.AddAsync(userToRegister, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            //Send Emails
            EmailHelper.SendEmail(model.Email, "Password", EmailHelper.GeneratePasswordEmailForUser(model.FirstName, password));
            EmailHelper.SendEmail(userToRegister.Email, "Card Information", EmailHelper.GenerateCardEmail(userToRegister.FirstName, card.CardNumber.Substring(12, 4), card.PIN)); ;
            EmailHelper.SendEmail(userToRegister.Email, "Email Confirmation", EmailHelper.CreateUrl(userToRegister.Email, secret));
        }

        private Card GenerateNewCard(string name)
        {
            var rnd = new Random();

            var cr = new Card
            {
                CardNumber = CardNumberHelper.GenerateCreditCardNumber(),
                FullName = name,
                ExpirationDate = DateTime.UtcNow.AddYears(4),
                PIN = (short)rnd.Next(1000, 9999),
                CVC = (short)rnd.Next(100, 999)
            };

            return cr;
        }
    }
}