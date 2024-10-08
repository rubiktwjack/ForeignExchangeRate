﻿using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;

namespace ForeignExchangeRate.Service.DAL
{
    public interface IRepository
    {
        Task<List<SP_SelectCurrencyExchangeRateResponse>> SP_SelectCurrencyExchangeRate(SP_SelectCurrencyExchangeRateRequest request);

        Task<List<SP_SelectCurrencyNameMappingResponse>> SP_SelectCurrencyNameMapping(SP_SelectCurrencyNameMappingRequest request);

        Task<List<SP_InsertCurrencyExchangeRateResponse>> SP_InsertCurrencyExchangeRate(SP_InsertCurrencyExchangeRateRequest request);

        Task<List<SP_AddCurrencyResponse>> SP_AddCurrency(SP_AddCurrencyRequest request);

        Task<List<SP_UpdateCurrencyResponse>> SP_UpdateCurrency(SP_UpdateCurrencyRequest request);

        Task<List<SP_DeleteCurrencyResponse>> SP_DeleteCurrency(SP_DeleteCurrencyRequest request);
    }
}
