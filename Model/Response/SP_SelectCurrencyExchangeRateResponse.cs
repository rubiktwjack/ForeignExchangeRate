namespace ForeignExchangeRate.Model.Response
{
    public class SP_SelectCurrencyExchangeRateResponse
    {
        public DateTime Date { get; set; }

        public string OriginalCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public float ExchangeRate { get; set; }       
    }
}
