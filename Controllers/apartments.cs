
using Apartments.Core.DTOs;
using Apartments.Core.Services;
using Apartments.Entitise;
using Apartments.Models;
using Apartmrnts.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;


namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apartments : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public apartments(IApartmentService apartmentService, IMapper map)
        {
            _apartmentService = apartmentService;
            _mapper = map;

        }
        // GET: api/<apartments>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var apartmentList = await _apartmentService.GetList();
            var apartment = _mapper.Map<IEnumerable<ApartmentDTO>>(apartmentList);
            return Ok(apartment);
        }

        // GET: api/<apartments>
        //[HttpGet ("second")]
        //public async Task<ActionResult> Get(int rooms,string city,string type)
        //{
        //    var apartmentList = _apartmentService.GetList();
        //    var apartment = _mapper.Map<IEnumerable<ApartmentDTO>>(apartmentList);
        //    return Ok(_apartmentService.GetList());
        //}



        // GET api/<apartments>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int ApartmentNum)
        {
            var apart = await _apartmentService.GetById(ApartmentNum);
            var apartment = _mapper.Map<IEnumerable<ApartmentDTO>>(apart);
            if (apart != null)
            {
                return Ok(apart);
            }
            return NotFound();
        }

        // POST api/<apartments>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ApartmentPostModel a)
        {
            var newApartment = _mapper.Map<apartment>(a);
             _apartmentService.Add(newApartment);

            return Ok();

        }

        //PUT api/<apartments>/5
        [HttpPut("{apartmentNum}")]
        public async Task <ActionResult> Put(int apartmentNum, [FromBody] ApartmentPostModel a)
        {
            
            var apart = await _apartmentService.Put(apartmentNum, _mapper.Map <apartment>(a));

            if (apart != null)
            {
                return Ok(apart);
            }
            return NotFound();
        }


        // DELETE api/<apartments>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int apartment_num)
        {
            var apart = _apartmentService.Remove(apartment_num);

            if (apart != null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}