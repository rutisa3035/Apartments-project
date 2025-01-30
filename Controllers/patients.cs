using Apartments.Core.DTOs;
using Apartments.Core.Services;
using Apartments.Data;
using Apartments.Entitise;
using Apartments.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class patients : ControllerBase
    {

        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public patients(IPatientService patientService, IMapper map)
        {
            _patientService = patientService;
            _mapper = map; 
        }

        // GET: api/<patients>
        [HttpGet]

        public async Task <ActionResult> Get()
        {
            var patientList = await _patientService.GetAll();
            var patients = _mapper.Map<IEnumerable<PatientDto>>(patientList);
            return Ok(patients);
        }

        // GET api/<patients>/5
        [HttpGet("{id}")]
        public async Task <ActionResult> Get(int id)
        {
            var pat = await _patientService.GetById(id);
            var patient = _mapper.Map<PatientDto>(pat);
            if (pat != null)
            {
                return Ok(pat);
            }
            return NotFound();
        }

        // POST api/<patients>
        [HttpPost]
        public async Task <ActionResult> Post([FromBody] PatientPostModel p)
        {
            var newPatient = _mapper.Map<patient>(p);
            await _patientService.Add(newPatient);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task <ActionResult> Put(int id, [FromBody] PatientPostModel p)
        {

            var pat = await _patientService.Put(id, _mapper.Map<patient>(p));
            if (pat != null)
            {
                return Ok(pat);

            }
            return NotFound();
        }

        // DELETE api/<patients>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pat = await _patientService.Remove(id);
            if (pat != null)
            {

                return Ok();
            }
            return NotFound();
        }
    }
}

