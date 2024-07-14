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
            var patientList = new List<Patient>();   
            var patient = new Patient
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 5, 15),
                MedicalRecordNumber = "MRN12345"
            };

            patientList.Add(patient);

            return patientList;
        }
    }
}
