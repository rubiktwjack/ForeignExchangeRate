﻿using ForeignExchangeRate.Helper;
using ForeignExchangeRate.Model;
using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;
using ForeignExchangeRate.Service.DAL;
using RestSharp;
using System.Globalization;
using System.Reflection;

namespace ForeignExchangeRate.Service.BLL
{
    public class ForeignExchangeService : IForeignExchangeService
    {
        private readonly IRepository _repository;
        public ForeignExchangeService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAndSetCurrencyExchangeRateResponse> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request)
        {
            GetAndSetCurrencyExchangeRateResponse result = new GetAndSetCurrencyExchangeRateResponse()
            {
                TargetCurrency = request.TargetCurrency,
                CurrencyExchangeRateList = new List<CurrencyExchangeRate>()
            };

            SP_SelectCurrencyNameMappingRequest sp_SelectCurrencyNameMappingRequest = new SP_SelectCurrencyNameMappingRequest()
            {
            };
            List<SP_SelectCurrencyNameMappingResponse> sp_SelectCurrencyNameMappingResponse = await _repository.SP_SelectCurrencyNameMapping(sp_SelectCurrencyNameMappingRequest);

            if (sp_SelectCurrencyNameMappingResponse.Any(x => x.Currency == request.TargetCurrency))
            {
                result.TargetCurrencyName = sp_SelectCurrencyNameMappingResponse
                                .Where(x => x.Currency == request.TargetCurrency)
                                .FirstOrDefault()
                                .Name;
            }
            else
            {
                result.TargetCurrencyName = "DB未有對應中文名稱";
            }

            SP_SelectCurrencyExchangeRateRequest sp_SelectCurrencyExchangeRateRequest = new SP_SelectCurrencyExchangeRateRequest()
            {
                //to do auto mapper
                QueryStartTime = request.QueryStartTime,
                QueryEndTime = request.QueryEndTime,
                TargetCurrency = request.TargetCurrency,
            };
            List<SP_SelectCurrencyExchangeRateResponse> sp_SelectCurrencyExchangeRateResponse = await _repository.SP_SelectCurrencyExchangeRate(sp_SelectCurrencyExchangeRateRequest);

            result.CurrencyExchangeRateList
                .AddRange(sp_SelectCurrencyExchangeRateResponse
                            .Where(x => x.Date >= request.QueryStartTime && x.Date <= request.QueryEndTime)
                            .Select(x => new CurrencyExchangeRate
                            {
                                Date = x.Date.ToString("yyyy-MM-dd"),
                                OriginalCurrency = x.OriginalCurrency,
                                ExchangeRate = x.ExchangeRate,
                            }));

            //API URL：https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates
            string baseUrl = "https://openapi.taifex.com.tw/";
            string resouseUrl = "v1/DailyForeignExchangeRates";
            string resquestBody = null;
            IRestResponse<List<ExchangeRate>> apiResponse = await APIHealper.Call<List<ExchangeRate>>(baseUrl, resouseUrl, resquestBody);
            List<ExchangeRateMapping> exchangeRateMappings = ExchangeRateMappings(apiResponse.Data);

            result.CurrencyExchangeRateList
                .AddRange(exchangeRateMappings
                            .Where(x => x.TargetCurrency == request.TargetCurrency &&
                                        x.Date >= request.QueryStartTime &&
                                        x.Date <= request.QueryEndTime)
                            .Select(x => new CurrencyExchangeRate
                            {
                                Date = x.Date.ToString("yyyy-MM-dd"),
                                OriginalCurrency = x.OriginalCurrency,
                                ExchangeRate = x.ExchangeRate
                            }));
            foreach (var item in result.CurrencyExchangeRateList)
            {
                if (sp_SelectCurrencyNameMappingResponse.Any(x => x.Currency == item.OriginalCurrency))
                {
                    item.OriginalCurrencyName = sp_SelectCurrencyNameMappingResponse
                                    .Where(x => x.Currency == item.OriginalCurrency)
                                    .FirstOrDefault()
                                    .Name;
                }
                else
                {
                    item.OriginalCurrencyName = "DB未有對應中文名稱";
                }

            }
            result.CurrencyExchangeRateList = result.CurrencyExchangeRateList.OrderBy(x => x.Date).ToList();

            //移除重複及非目標幣別後新增
            exchangeRateMappings = exchangeRateMappings.Where(x => x.TargetCurrency == request.TargetCurrency).ToList();
            for (int i = exchangeRateMappings.Count - 1; i >= 0; i--)
            {
                if (sp_SelectCurrencyExchangeRateResponse
                        .Any(x => x.TargetCurrency == exchangeRateMappings[i].TargetCurrency &&
                                  x.OriginalCurrency == exchangeRateMappings[i].OriginalCurrency &&
                                  x.ExchangeRate == exchangeRateMappings[i].ExchangeRate &&
                                  x.Date == exchangeRateMappings[i].Date))
                {
                    exchangeRateMappings.RemoveAt(i);
                }
            }

            foreach (var exchangeRateMapping in exchangeRateMappings)
            {
                SP_InsertCurrencyExchangeRateRequest sp_InsertCurrencyExchangeRateRequest = new SP_InsertCurrencyExchangeRateRequest()
                {
                    Date = exchangeRateMapping.Date,
                    OriginalCurrency = exchangeRateMapping.OriginalCurrency,
                    TargetCurrency = exchangeRateMapping.TargetCurrency,
                    ExchangeRate = exchangeRateMapping.ExchangeRate,
                };
                List<SP_InsertCurrencyExchangeRateResponse> sp_InsertCurrencyExchangeRateResponse = await _repository.SP_InsertCurrencyExchangeRate(sp_InsertCurrencyExchangeRateRequest);
            }

            return result;
        }

        public List<ExchangeRateMapping> ExchangeRateMappings(List<ExchangeRate> exchangeRate)
        {
            List<ExchangeRateMapping> result = new List<ExchangeRateMapping>();
            exchangeRate.ForEach(item =>
            {
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();
                string date = item.Date;

                foreach (PropertyInfo property in properties)
                {
                    string name = property.Name, targetCurrency = "", originalCurrency = "";
                    object value = property.GetValue(item);

                    if (name == "Date")
                    {
                        continue;
                    }
                    else
                    {
                        targetCurrency = name.Substring(0, 3);
                        originalCurrency = name.Substring(name.Length - 3);
                    }

                    result.Add(new ExchangeRateMapping
                    {
                        Date = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture),
                        TargetCurrency = targetCurrency,
                        OriginalCurrency = originalCurrency,
                        ExchangeRate = float.Parse(value.ToString())
                    });
                }
            });

            return result;
        }

        public async Task<AddCurrencyResponse> AddCurrency(AddCurrencyRequest request)
        {
            SP_AddCurrencyRequest sp_AddCurrencyRequest = new SP_AddCurrencyRequest()
            {
                Currency = request.Currency,
                Name = request.Name,
            };
            List<SP_AddCurrencyResponse> sp_AddCurrencyResponse = await _repository.SP_AddCurrency(sp_AddCurrencyRequest);

            AddCurrencyResponse response = new AddCurrencyResponse();
            return response;
        }

        public async Task<UpdateCurrencyResponse> UpdateCurrency(UpdateCurrencyRequest request)
        {
            SP_UpdateCurrencyRequest sp_UpdateCurrencyRequest = new SP_UpdateCurrencyRequest()
            {
                Currency = request.Currency,
                Name = request.Name,
            };
            List<SP_UpdateCurrencyResponse> sp_UpdateCurrencyResponse = await _repository.SP_UpdateCurrency(sp_UpdateCurrencyRequest);

            UpdateCurrencyResponse response = new UpdateCurrencyResponse();
            return response;
        }

        public async Task<DeleteCurrencyResponse> DeleteCurrency(DeleteCurrencyRequest request)
        {
            SP_DeleteCurrencyRequest sp_DeleteCurrencyRequest = new SP_DeleteCurrencyRequest()
            {
                Currency = request.Currency,             
            };
            List<SP_DeleteCurrencyResponse> sp_DeleteCurrencyResponse = await _repository.SP_DeleteCurrency(sp_DeleteCurrencyRequest);

            DeleteCurrencyResponse response = new DeleteCurrencyResponse();
            return response;
        }
    }
}
