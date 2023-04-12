using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Operator;
using BankingManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

namespace BankingManagement.Persistence.Seed
{
    public static class DatabaseSeeding
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<BankingManagementDbContext>();
            database.Database.EnsureCreated();
            Seed(database);
        }

        private static void Seed(BankingManagementDbContext database)
        {
            var seeded = false;

            var operators = new List<Operator>
        {
            new Operator
            {
                FirstName = "Avtandil",
                LastName = "Lazishvili",
                PrivateNumber = "61004067844",
                DateOfBirth = new DateTime(2000, 10, 7),
                Email = "avtukalaz@gmail.com",
                PasswordHash = GenerateSHA512Hash("Abcd123!"),
                Role = OperatorRoles.Administrator,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            }
        };

            foreach (var oper in operators)
            {
                if (database.Operators.AnyAsync(x => x.Email == oper.Email || x.PrivateNumber == oper.PrivateNumber).Result)
                    continue;

                database.Operators.Add(oper);
                seeded = true;
            }

            if (seeded)
                database.SaveChanges();
        }

        private static string GenerateSHA512Hash(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha512.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (var item in hashBytes)
                {
                    sb.Append(item.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}