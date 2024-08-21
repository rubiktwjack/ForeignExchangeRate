namespace ForeignExchangeRate.Model.Response
{
    public class GetAndSetCurrencyExchangeRateResponse
    {
        public string TargetCurrency { get; set; }

        public string TargetCurrencyName { get; set; }

        public List<CurrencyExchangeRate> CurrencyExchangeRateList { get; set; }
    }

    public class CurrencyExchangeRate
    {
        public string Date { get; set; }

        public string OriginalCurrency { get; set; }

        public string OriginalCurrencyName { get; set; }

        public float ExchangeRate { get; set; }
    }
}
