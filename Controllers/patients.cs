using Apartments.Core.Services;
using Apartments.Entitise;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class patients : ControllerBase
    {

        private readonly IPatientService _patientService;
        public patients(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/<patients>
        [HttpGet]

        public ActionResult Get()
        {
            return Ok(_Context.patient);
        }

        // GET api/<patients>/5
        [HttpGet("{id}")]


      

        public ActionResult Getid(int id)
        {
            var patient = _Context.patient.Find(e => e.Id == id);
            if (patient == null)
            {
                return NotFound();  
            }
            return Ok(patient);  
        }




        // POST api/<patients>
        [HttpPost]
        public ActionResult Post([FromBody] patient value)
        {
            _Context.patient.Add(value);
            return Ok(value);
        }

        // PUT api/<patients>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] patient value)
        {
            var index = _Context.patient.FindIndex(e => e.Id == id);
            _Context.patient[index].Name = value.Name;
            _Context.patient[index].Phone_number = value.Phone_number;
            _Context.patient[index].Email = value.Email;
            _Context.patient[index].Adress = value.Adress;
            _Context.patient[index].Preferred_area = value.Preferred_area;

            return Ok(_Context.patient[index]);
        }

        // DELETE api/<patients>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
