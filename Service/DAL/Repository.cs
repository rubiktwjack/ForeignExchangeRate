using ForeignExchangeRate.Helper.DBHelper;
using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;

namespace ForeignExchangeRate.Service.DAL
{
    public class Repository : IRepository
    {
        private readonly IDBHelper _dbHelper;
        public Repository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<SP_SelectCurrencyExchangeRateResponse>> SP_SelectCurrencyExchangeRate(SP_SelectCurrencyExchangeRateRequest request)
        {
            List<SP_SelectCurrencyExchangeRateResponse> response =
                await _dbHelper.ExecuteSP<SP_SelectCurrencyExchangeRateRequest, SP_SelectCurrencyExchangeRateResponse>("[dbo].[SelectCurrencyExchangeRate]", request);
            return response;
        }

        public async Task<List<SP_SelectCurrencyNameMappingResponse>> SP_SelectCurrencyNameMapping(SP_SelectCurrencyNameMappingRequest request)
        {
            List<SP_SelectCurrencyNameMappingResponse> response =
                await _dbHelper.ExecuteSP<SP_SelectCurrencyNameMappingRequest, SP_SelectCurrencyNameMappingResponse>("[dbo].[SelectCurrencyNameMapping]", request);
            return response;
        }
    }
}
