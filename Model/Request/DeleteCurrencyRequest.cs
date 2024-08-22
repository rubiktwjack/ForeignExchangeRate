using System.ComponentModel.DataAnnotations;

namespace ForeignExchangeRate.Model.Request
{
    public class DeleteCurrencyRequest
    {
        [Required]
        public string Currency { get; set; }
    }
}
