using ForeignExchangeRate.Helper.DBHelper;
using System.Data;
using System.Data.Common;

namespace ForeignExchangeRate.Model.Request
{
    public class SP_SelectCurrencyExchangeRateRequest
    {
        [DBParameter(Type = DbType.DateTime2)]
        public DateTime QueryStartTime { get; set; }

        [DBParameter(Type = DbType.DateTime2)]
        public DateTime QueryEndTime { get; set; }

        [DBParameter(Type = DbType.String)]
        public string TargetCurrency { get; set; }
    }
}
