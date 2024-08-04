using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using HospitalMiddleware.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HospitalMiddleware.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly RedisCacheService _cache;
        private readonly ILogger<HospitalController> _logger;
        private readonly IHospitalService _hospitalService;
        private readonly IEncryptionService _encryptionService;

        public HospitalController(ILogger<HospitalController> logger, RedisCacheService cache, IHospitalService hospitalService, IEncryptionService encryptionService)
        {
            _cache = cache;
            _logger = logger;
            _hospitalService = hospitalService;
        }

        [HttpGet(Name = "GetHospitalDetails")]
        public async Task<ActionResult<object>> GetHospitalDetails( string name)
        {

            var response = _hospitalService.GetHosptialByName(name);

            if (response != null && response.Count() > 0)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));

                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet(Name = "GetHospitalDetailsById")]
        public async Task<ActionResult<object>> GetHospitalDetailsById(string Id)
        {

            var response = _hospitalService.GetHospitalDetailsById(Id);

            if (response != null)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));

                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
