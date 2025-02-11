﻿using HospitalMiddleware.Interfaces;
using HospitalMiddleware.Model;
using HospitalMiddleware.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;

namespace HospitalMiddleware.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly RedisCacheService _cache;
        private readonly IEncryptionService _encryptionService;
        private readonly PatientService _patientService;

        public PatientController(RedisCacheService cache, IEncryptionService encryptionService, PatientService patientService)
        {
            _cache = cache;
            _encryptionService = encryptionService;
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var cachedPatient = await _cache.GetAsync(id.ToString());
            if (cachedPatient != null)
            {
                var decryptedData = _encryptionService.Decrypt(cachedPatient);
                return Ok(decryptedData);
            }

            // Dummy data for demonstration
            var patient = new Patient
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 5, 15),
                MedicalRecordNumber = "MRN12345"
            };

            var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(patient));
            await _cache.SetAsync(id.ToString(), encryptedData, TimeSpan.FromMinutes(5));

            return Ok(patient);
        }

        [HttpGet(Name = "GetPatientByName")]
        public async Task<ActionResult<object>> GetPatientByName(string name)
        {

            var response = _patientService.GetPatientByName(name);

            if (response != null && response.Count() > 0)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));

                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet(Name = "GetPatientByDetailsById")]
        public async Task<ActionResult<object>> GetPatientByDetailsById(string name)
        {

            var response = _patientService.GetPatientByDetailsById(name);

            if (response != null && response.Count() > 0)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));

                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet(Name = "GetActivePatients")]
        public async Task<ActionResult<Patient>> GetActivePatients()
        {

            var response = _patientService.GetActivePatients();

            if (response != null)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));

                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet(Name = "PulllActivePatients")]// fix this tomorrow
        public async Task<ActionResult<Patient>> PulllActivePatients()
        {

            var response = _patientService.GetActivePatients();

            if (response != null)
            {
                var encryptedData = _encryptionService.Encrypt(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(response.ToString(), encryptedData, TimeSpan.FromMinutes(5));

                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete(Name = "DeletePatients")]// fix this tomorrow
        public ActionResult DeletePatients()
        {

            var response = _patientService.GetActivePatients();

            if (response != null)
            {

                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
