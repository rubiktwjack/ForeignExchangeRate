using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;
using ForeignExchangeRate.Service.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ForeignExchangeRate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForeignExchangeRateController : ControllerBase
    {
        private readonly IForeignExchangeService _foreignExchangeService;
        public ForeignExchangeRateController(IForeignExchangeService foreignExchangeService)
        {
            _foreignExchangeService = foreignExchangeService;
        }

        /// <summary>
        /// �d�ߩҦ��ײv
        /// </summary>  
        [HttpPost]
        public async Task<IActionResult> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request)
        {
            GetAndSetCurrencyExchangeRateResponse response = await _foreignExchangeService.GetAndSetCurrencyExchangeRate(request);
            return Ok(response);
        }
    }
}
