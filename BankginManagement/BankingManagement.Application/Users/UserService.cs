using BankingManagement.Application.Accounts;
using BankingManagement.Application.Cards;
using BankingManagement.Application.Infrastructure.Helpers;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Users.Requests;
using BankingManagement.Domain.Card;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.User;
using IbanNet.Registry;
using Mapster;

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
                throw new Exception("Email or password is invalid");
            if (!user.EmailConfirmed)
                throw new Exception("Email is not confirmed");

            return user;
        }

        public async Task RegisterAsync(UserRegisterRequestModel model, CancellationToken cancellationToken)
        {
            var userToRegister = model.Adapt<User>();

            var password = MyPasswordHelper.GenerateRandomPassword();

            userToRegister.PasswordHash = MyPasswordHelper.GenerateSHA512Hash(password);

            int gelCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.GEL).Count();
            if (gelCount > 1)
                throw new Exception("Cannot have more than one GEL account while registering user");
            int usdCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.USD).Count();
            if (gelCount > 1)
                throw new Exception("Cannot have more than one USD account while registering user");
            int eurCount = userToRegister.Accounts.Where(x => x.Currency == Currencies.EUR).Count();
            if (gelCount > 1)
                throw new Exception("Cannot have more than one EUR account while registering user");

            //Card Generation
            Card card;
            do
            {
                card = GenerateNewCard(userToRegister.FirstName + "" + userToRegister.LastName);
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
        }

        private Card GenerateNewCard(string name)
        {
            var rnd = new Random();

            var cr = new Card
            {
                CardNumber = CardNumberHelper.GenerateCreditCardNumber(),
                FullName = name,
                ExpirationDate = DateTime.Now.AddYears(4),
                PIN = (short)rnd.Next(1000, 9999),
                CVC = (short)rnd.Next(100, 999)
            };

            return cr;
        }
    }
}