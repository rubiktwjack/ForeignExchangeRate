using ForeignExchangeRate.Helper.DBHelper;
using System.Data;

namespace ForeignExchangeRate.Model.Request
{
    public class SP_InsertCurrencyExchangeRateRequest
    {
        [DBParameter(Type = DbType.DateTime2)]
        public DateTime Date { get; set; }

        [DBParameter(Type = DbType.String)]
        public string OriginalCurrency { get; set; }

        [DBParameter(Type = DbType.String)]
        public string TargetCurrency { get; set; }

        [DBParameter(Type = DbType.Double)]
        public float ExchangeRate { get; set; }
    }
}
