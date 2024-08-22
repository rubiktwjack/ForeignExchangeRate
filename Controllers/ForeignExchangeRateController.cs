using ForeignExchangeRate.Model.Request;
using ForeignExchangeRate.Model.Response;
using ForeignExchangeRate.Service.BLL;
using Microsoft.AspNetCore.Mvc;

namespace ForeignExchangeRate.Controllers
{
    /// <summary>
    /// ���
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
        /// �d�ߨó]�w�ײv
        /// �d�ߩҦ��ײv�A�îھڦ^������Ƨ�sDB
        /// </summary>
        /// <param name="request">�d�߻P�]�w�ײv���ШD�Ѽ�</param>
        /// <returns>���G</returns>
        [HttpPost]
        public async Task<IActionResult> GetAndSetCurrencyExchangeRate(GetAndSetCurrencyExchangeRateRequest request)
        {
            GetAndSetCurrencyExchangeRateResponse response = await _foreignExchangeService.GetAndSetCurrencyExchangeRate(request);
            return Ok(response);
        }

        /// <summary>
        /// �s�W���O
        /// </summary>
        /// <param name="request">�s�W���O�Ѽ�</param>
        /// <returns>�s�W���G</returns>
        [HttpPost]
        public async Task<IActionResult> AddCurrency(AddCurrencyRequest request)
        {
            AddCurrencyResponse response = await _foreignExchangeService.AddCurrency(request);
            return Ok(response);
        }

        /// <summary>
        /// ��s���O
        /// </summary>
        /// <param name="request">��s���O�Ѽ�</param>
        /// <returns>��s���G</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCurrency(UpdateCurrencyRequest request)
        {
            UpdateCurrencyResponse response = await _foreignExchangeService.UpdateCurrency(request);
            return Ok(response);
        }

        /// <summary>
        /// �R�����O
        /// </summary>
        /// <param name="request">�R�����O�Ѽ�</param>
        /// <returns>�R�����G</returns>
        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(DeleteCurrencyRequest request)
        {
            DeleteCurrencyResponse response = await _foreignExchangeService.DeleteCurrency(request);
            return Ok(response);
        }
    }
}