using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalMiddleware.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HospitalController : ControllerBase
    {

        private readonly ILogger<HospitalController> _logger;
        private readonly IHospitalService _hospitalService;

        public HospitalController(ILogger<HospitalController> logger, IHospitalService hospitalService)
        {
            _logger = logger;
            _hospitalService = hospitalService;
        }

        [HttpGet(Name = "GetHospitalDetails")]
        public IEnumerable<object> GetHospitalDetails( string name)
        {

            var response = _hospitalService.GetHosptialByName(name);

            return response;
        }
    }
}
