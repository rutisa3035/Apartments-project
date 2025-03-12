using Apartments.Core.DTOs;
using Apartments.Core.Entitise;
using Apartments.Core.Services;
using Apartments.Data;
using Apartments.Entitise;
using Apartments.Models;
using Apartmrnts.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUsersServicece _usersService;

        public patients(IPatientService patientService, IMapper map, IUsersServicece usersService)
        {
            _patientService = patientService;
            _mapper = map;
            _usersService  = usersService;
        }

        // GET: api/<patients>
        [HttpGet]
        [AllowAnonymous]
        public async Task <ActionResult> Get()
        {
            var patientList = await _patientService.GetAll();
            var patients = _mapper.Map<IEnumerable<PatientDto>>(patientList);
            return Ok(patients);
        }

        // GET api/<patients>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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
        //[HttpGet("is-exist")]
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task <ActionResult> Post([FromBody] PatientPostModel p)
        {
            var newPatient = _mapper.Map<patient>(p);

            if(await _usersService.GetByName(p.Name, p.Password) == null) { 
                var user1 = new Users { Password = p.Password, UserName = p.Name, Role = Users.eRole.patient };
                newPatient.user = user1;
                newPatient.UserId = user1.Id;
                await _usersService.ADD(user1);
            }
            else
            {
                var user2 = await _usersService.GetByName(p.Name, p.Password);
                newPatient.user = user2;
                newPatient.UserId = user2.Id;
            }
            await _patientService.Add(newPatient);
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Patient")]
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
        [Authorize(Roles = "Patient")]
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

