using Newtonsoft.Json;

namespace ForeignExchangeRate.Model
{
    public class ExchangeRate
    {
        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("USD/NTD")]
        public string USDNTD { get; set; }

        [JsonProperty("RMB/NTD")]
        public string RMBNTD { get; set; }

        [JsonProperty("EUR/USD")]
        public string EURUSD { get; set; }

        [JsonProperty("USD/JPY")]
        public string USDJPY { get; set; }

        [JsonProperty("GBP/USD")]
        public string GBPUSD { get; set; }

        [JsonProperty("AUD/USD")]
        public string AUDUSD { get; set; }

        [JsonProperty("USD/HKD")]
        public string USDHKD { get; set; }

        [JsonProperty("USD/RMB")]
        public string USDRMB { get; set; }

        [JsonProperty("USD/ZAR")]
        public string USDZAR { get; set; }

        [JsonProperty("NZD/USD")]
        public string NZDUSD { get; set; }
    }
}
