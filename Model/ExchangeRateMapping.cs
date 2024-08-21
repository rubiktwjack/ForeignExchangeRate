namespace ForeignExchangeRate.Model
{
    public class ExchangeRateMapping
    {
        public DateTime Date { get; set; }

        public string OriginalCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public string ExchangeRate { get; set; }

    }
}
