namespace BankingManagementATM.API.Infrastucture.Auth.JWT
{
    public class JWTConfiguration
    {
        public string Secret { get; set; }
        public int Expires { get; set; }
    }
}
