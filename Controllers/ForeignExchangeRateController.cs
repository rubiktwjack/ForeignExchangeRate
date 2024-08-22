using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;
using ForeignExchangeRate.Service.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ForeignExchangeRate.Controllers
{
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
        /// 查詢所有匯率
        /// </summary>  
        [HttpPost]
        public async Task<IActionResult> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request)
        {
            GetAndSetCurrencyExchangeRateResponse response = await _foreignExchangeService.GetAndSetCurrencyExchangeRate(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(AddCurrencyRequest request)
        {
            AddCurrencyResponse response = await _foreignExchangeService.AddCurrency(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCurrency(UpdateCurrencyRequest request)
        {
            UpdateCurrencyResponse response = await _foreignExchangeService.UpdateCurrency(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(DeleteCurrencyRequest request)
        {
            DeleteCurrencyResponse response = await _foreignExchangeService.DeleteCurrency(request);
            return Ok(response);
        }
    }
}
