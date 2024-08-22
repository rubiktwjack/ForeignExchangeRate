using ForeignExchangeRate.Helper.DBHelper;
using System.Data;

namespace ForeignExchangeRate.Model.Request
{
    public class SP_DeleteCurrencyRequest
    {
        [DBParameter(Type = DbType.String)]
        public string Currency { get; set; }
    }
}
