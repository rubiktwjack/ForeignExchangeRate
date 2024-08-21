using System.ComponentModel.DataAnnotations;

namespace ForeignExchangeRate.Model.Request
{
    public class GetAndSetCurrencyExchangeRateRequest
    {
        [Required]
        public DateTime QueryStartTime { get; set; }

        [Required]
        public DateTime QueryEndTime { get; set; }

        [Required]
        public string TargetCurrency { get; set; }
    }
}
