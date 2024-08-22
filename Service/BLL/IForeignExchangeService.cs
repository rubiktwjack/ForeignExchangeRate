using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;

namespace ForeignExchangeRate.Service.BLL
{
    public interface IForeignExchangeService
    {
        Task<GetAndSetCurrencyExchangeRateResponse> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request);

        Task<AddCurrencyResponse> AddCurrency(AddCurrencyRequest request);

        Task<UpdateCurrencyResponse> UpdateCurrency(UpdateCurrencyRequest request);

        Task<DeleteCurrencyResponse> DeleteCurrency(DeleteCurrencyRequest request);
    }
}
