namespace BankingManagement.InsideSystem.API.Infrastucture.Auth
{
    public class JWTConfiguration
    {
        public string Secret { get; set; }
        public int Expires { get; set; }
    }
}