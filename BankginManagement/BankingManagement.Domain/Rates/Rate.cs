namespace BankingManagement.Domain.Rates
{
    public class Rate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Quantity { get; set; }
        public double RateFormated { get; set; }
    }
}