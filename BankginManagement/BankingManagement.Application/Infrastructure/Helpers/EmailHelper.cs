using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace BankingManagement.Application.Infrastructure.Helpers
{
    public static class EmailHelper
    {
        public static void SendEmail(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");

            Execute(subject, htmlMessage, email);
        }

        public static void Execute(string subject, string body, string toEmail)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("eventsitacademy@gmail.com", "hzrwkqoardlculrg");
            smtp.EnableSsl = true;

            smtp.Send("eventsitacademy@gmail.com", toEmail, subject, body);
        }

        public static string GeneratePasswordEmailForUser(string firstName, string password)
        {
            return $"Dear {firstName},\nThank you for trusting and choosing our bank.\nYour password for internet banking " +
                $"is next: \"{password}\"\nPlease change it ASAP for security reasons";
        }

        public static string GeneratePasswordEmailForOperaotor(string firstName, string password)
        {
            return $"Dear {firstName},\nCongratulations to starting your new job with us.\nYour password for inside systems" +
                $"is next: \"{password}\"\nPlease change it ASAP for security reasons";
        }

        public static string GenerateCardEmail(string firstName, string number, int pin)
        {
            return $"Dear {firstName},\nPIN for credit card ending with {number} is: {pin}\nPlease change it ASAP";
        }

        public static string CreateUrl(string email, string secret)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),

                Expires = DateTime.Now.AddMinutes(15),
                Audience = "User",
                Issuer = "EmailConfirmation",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHanlder.CreateToken(tokenDescription);

            var tokenString = tokenHanlder.WriteToken(token);

            var url = $"https://localhost:7295/api/User/ConfirmEmail?code={tokenString}";

            return url;
        }
    }
}