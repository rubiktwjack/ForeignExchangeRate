using ForeignExchangeRate.Helper.DBHelper;
using System.Data;

namespace ForeignExchangeRate.Model.Request
{
    public class SP_UpdateCurrencyRequest
    {
        [DBParameter(Type = DbType.String)]
        public string Currency { get; set; }

        [DBParameter(Type = DbType.String)]
        public string Name { get; set; }
    }
}
