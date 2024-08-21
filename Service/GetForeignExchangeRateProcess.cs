using ForeignExchangeRate.Helper;
using ForeignExchangeRate.Model;
using Newtonsoft.Json;
using RestSharp;

namespace ForeignExchangeRate.Service
{
    public class GetForeignExchangeRateProcess
    {
        public async Task<List<ExchangeRate>> GetDailyForeignExchangeRatesAsync()
        {
            //呼叫API範例
            /*string baseUrl;
            string resouseUrl;
            string resquestBody;
            IRestResponse<APIRESPONSE> apiResponse = await APIHealper.Call<APIRESPONSE>(baseUrl, resouseUrl, resquestBody);*/
            
            // 呼叫 API 的 URL
            string apiUrl = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
            List<ExchangeRate> result = new List<ExchangeRate>();

            HttpClient client = new HttpClient();
            try
            {
                // 發送 Get 請求
                var response = await client.GetAsync(apiUrl);

                // 確保獲得 200 OK 回應
                response.EnsureSuccessStatusCode();

                // 解析回應
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<ExchangeRate>>(content);

                return result;
            }
            catch (HttpRequestException e)
            {
                return result;
            }
        }
    }
}
