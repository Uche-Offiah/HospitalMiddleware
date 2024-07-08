using Microsoft.AspNetCore.Mvc;

namespace HospitalMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HospitalController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HospitalController> _logger;

        public HospitalController(ILogger<HospitalController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetHospitalDetails")]
        public IEnumerable<object> Get()
        {
            return null;
        }
    }
}
