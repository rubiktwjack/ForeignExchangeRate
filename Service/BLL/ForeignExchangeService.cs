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
                                ExchangeRate = float.Parse(x.ExchangeRate)
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
                        ExchangeRate = value.ToString()
                    });
                }
            });

            return result;
        }
    }
}
