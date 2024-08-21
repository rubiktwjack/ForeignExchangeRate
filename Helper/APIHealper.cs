using Newtonsoft.Json;
using RestSharp;

namespace ForeignExchangeRate.Helper
{
    public static class APIHealper
    {
        public static async Task<IRestResponse<TResult>> Call<TResult>(string baseUrl, string resouceUrl, object requestBody)
        {
#if DEBUG
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // 忽略所有 SSL 錯誤
#endif
            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(resouceUrl, Method.POST, DataFormat.Json);
            request.AddJsonBody(requestBody);

            IRestResponse<TResult> result = await client.ExecuteAsync<TResult>(request);
            result.Data = JsonConvert.DeserializeObject<TResult>(result.Content);
            
            return result;
        }
    }
}
