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

        public async Task<List<SP_InsertCurrencyExchangeRateResponse>> SP_InsertCurrencyExchangeRate(SP_InsertCurrencyExchangeRateRequest request)
        {
            List<SP_InsertCurrencyExchangeRateResponse> response =
                await _dbHelper.ExecuteSP<SP_InsertCurrencyExchangeRateRequest, SP_InsertCurrencyExchangeRateResponse>("[dbo].[InsertCurrencyExchangeRate]", request);
            return response;
        }

        public async Task<List<SP_AddCurrencyResponse>> SP_AddCurrency(SP_AddCurrencyRequest request)
        {
            List<SP_AddCurrencyResponse> response =
                await _dbHelper.ExecuteSP<SP_AddCurrencyRequest, SP_AddCurrencyResponse>("[dbo].[AddCurrency]", request);
            return response;
        }

        public async Task<List<SP_UpdateCurrencyResponse>> SP_UpdateCurrency(SP_UpdateCurrencyRequest request)
        {
            List<SP_UpdateCurrencyResponse> response =
                await _dbHelper.ExecuteSP<SP_UpdateCurrencyRequest, SP_UpdateCurrencyResponse>("[dbo].[UpdateCurrency]", request);
            return response;
        }

        public async Task<List<SP_DeleteCurrencyResponse>> SP_DeleteCurrency(SP_DeleteCurrencyRequest request)
        {
            List<SP_DeleteCurrencyResponse> response =
                await _dbHelper.ExecuteSP<SP_DeleteCurrencyRequest, SP_DeleteCurrencyResponse>("[dbo].[DeleteCurrency]", request);
            return response;
        }
    }
}
