using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;

namespace ForeignExchangeRate.Service.BLL
{
    public interface IForeignExchangeService
    {
        Task<GetAndSetCurrencyExchangeRateResponse> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request);
    }
}
