using System.Text;

namespace BankingManagement.Application.Infrastructure.Helpers
{
    public static class CardNumberHelper
    {
        private static readonly string[] prefixList = new[] { "51", "52", "53", "54", "55" };

        public static string GenerateCreditCardNumber()
        {
            int randomPrefix = new Random().Next(0, prefixList.Length - 1);

            string prefix = prefixList[randomPrefix];

            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);
            Random rnd = new Random();

            while (sb.Length < 15)
            {
                sb.Append(rnd.Next(0, 10));
            }

            var reversedCCnumberstring = sb.ToString().ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            int sum = 0;
            int pos = 0;
            int[] reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < 15)
            {
                int odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != 14)
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            int checkdigit =
                Convert.ToInt32((Math.Floor((decimal)sum / 10) + 1) * 10 - sum) % 10;

            sb.Append(checkdigit);

            return sb.ToString();
        }
    }
}