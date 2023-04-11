namespace Rate.Wroker
{
    internal class RateRequestModel
    {
        public DateTime Date { get; set; }
        public List<Currency> Currencies { get; set; }
    }

    internal class Currency
    {
        public string Code { get; set; }
        public double Quantity { get; set; }
        public double RateFormated { get; set; }
        public double DiffFormated { get; set; }
        public double Rate { get; set; }
        public string Name { get; set; }
        public double Diff { get; set; }
        public DateTime Date { get; set; }
        public DateTime ValidFromDate { get; set; }
    }
}