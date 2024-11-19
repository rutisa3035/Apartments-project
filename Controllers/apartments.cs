using Apartments.Core.Services;
using Apartments.Entitise;
using Apartmrnts.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apartments : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public apartments(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: api/<apartments>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_apartmentService.GetList());
        }

        // GET api/<apartments>/5
        [HttpGet("{id}")]

        public ActionResult Getid(int apartmentNum)
        {
            var apartment = _Context.apartment.Find(e => e.ApartmentNum == apartmentNum);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(apartment);
        }

        // POST api/<apartments>
        [HttpPost]
        public ActionResult Post([FromBody] apartment value)
        {
            _Context.apartment.Add(value);
            return Ok(value);
        }

        // PUT api/<apartments>/5
        [HttpPut("{id}")]
        public ActionResult Put(int apartmentNum, [FromBody] apartment value)
        {
            var index = _Context.apartment.FindIndex(e => e.ApartmentNum == apartmentNum);
            _Context.apartment[index].City = value.City;
            _Context.apartment[index].NumRooms = value.NumRooms;
            _Context.apartment[index].Size = value.Size;
            _Context.apartment[index].Floor = value.Floor;
            _Context.apartment[index].Type = value.Type;
            _Context.apartment[index].Amount = value.Amount;
            return Ok(_Context.apartment[index]);

        }

        // DELETE api/<apartments>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
