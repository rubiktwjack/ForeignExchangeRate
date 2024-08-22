using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;
using ForeignExchangeRate.Service.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ForeignExchangeRate.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ForeignExchangeRateController : ControllerBase
    {
        private readonly IForeignExchangeService _foreignExchangeService;

        public ForeignExchangeRateController(IForeignExchangeService foreignExchangeService)
        {
            _foreignExchangeService = foreignExchangeService;
        }

        /// <summary>
        /// 查詢並設定匯率
        /// 查詢所有匯率，並根據回應的資料更新DB
        /// </summary>
        /// <param name="request">查詢與設定匯率的請求參數</param>
        /// <returns>結果</returns>
        [HttpPost]
        public async Task<IActionResult> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request)
        {
            GetAndSetCurrencyExchangeRateResponse response = await _foreignExchangeService.GetAndSetCurrencyExchangeRate(request);
            return Ok(response);
        }

        /// <summary>
        /// 新增幣別
        /// </summary>
        /// <param name="request">新增幣別參數</param>
        /// <returns>新增結果</returns>
        [HttpPost]
        public async Task<IActionResult> AddCurrency(AddCurrencyRequest request)
        {
            AddCurrencyResponse response = await _foreignExchangeService.AddCurrency(request);
            return Ok(response);
        }

        /// <summary>
        /// 更新幣別
        /// </summary>
        /// <param name="request">更新幣別參數</param>
        /// <returns>更新結果</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCurrency(UpdateCurrencyRequest request)
        {
            UpdateCurrencyResponse response = await _foreignExchangeService.UpdateCurrency(request);
            return Ok(response);
        }

        /// <summary>
        /// 刪除幣別
        /// </summary>
        /// <param name="request">刪除幣別參數</param>
        /// <returns>刪除結果</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(DeleteCurrencyRequest request)
        {
            DeleteCurrencyResponse response = await _foreignExchangeService.DeleteCurrency(request);
            return Ok(response);
        }
    }
}