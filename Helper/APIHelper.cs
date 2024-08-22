using Newtonsoft.Json;
using RestSharp;

namespace ForeignExchangeRate.Helper
{
    /// <summary>
    /// APIHelper
    /// </summary>
    public static class APIHelper
    {
        /// <summary>
        /// 呼叫外部 API 並將結果反序列化成指定的型別
        /// </summary>
        /// <typeparam name="TResult">API 結果的型別</typeparam>
        /// <param name="baseUrl">API 的基底 URL。</param>
        /// <param name="resourceUrl">API 的資源 URL。</param>
        /// <param name="requestBody">傳送給 API 的請求物件</param>
        /// <returns>API結果</returns>
        public static async Task<IRestResponse<TResult>> Call<TResult>(string baseUrl, string resourceUrl, object requestBody)
        {
#if DEBUG
            // 開發模式忽略所有 SSL 驗證錯誤 (僅限開發環境使用)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
#endif

            RestClient client = new RestClient(baseUrl);
            RestRequest request = new RestRequest(resourceUrl, Method.POST, DataFormat.Json);
            request.AddJsonBody(requestBody);

            IRestResponse<TResult> result = await client.ExecuteAsync<TResult>(request);
            result.Data = JsonConvert.DeserializeObject<TResult>(result.Content);

            return result;
        }
    }
}