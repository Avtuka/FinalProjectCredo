using System.Security.Cryptography;
using System.Text;

namespace BankingManagement.Application.Infrastructure.Helpers
{
    internal static class MyPasswordHelper
    {
        internal static string GenerateSHA512Hash(string password)
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

        internal static string GenerateRandomPassword()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            char ch;

            for (int i = 0; i < 10; i++)
            {
                ch = Convert.ToChar(rnd.Next(33, 126));
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}