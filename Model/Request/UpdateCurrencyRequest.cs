using System.ComponentModel.DataAnnotations;

namespace ForeignExchangeRate.Model.Request
{
    public class UpdateCurrencyRequest
    {
        [Required]
        public string Currency { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
