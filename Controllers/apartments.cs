
using Apartments.Core.DTOs;
using Apartments.Core.Services;
using Apartments.Entitise;
using Apartments.Models;
using Apartmrnts.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var apartmentList = await _apartmentService.GetList();
            var apartment = _mapper.Map<IEnumerable<ApartmentDTO>>(apartmentList);
            return Ok(apartment);
        }

        // GET api/<apartments>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var apart = await _apartmentService.GetById(id);
            var apartment = _mapper.Map<ApartmentDTO>(apart);
            if (apart != null)
            {
                return Ok(apart);
            }
            return NotFound();
        }

        // POST api/<apartments>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ApartmentPostModel a)
        {
            var newApartment = _mapper.Map<apartment>(a);
             _apartmentService.Add(newApartment);

            return Ok();

        }

        //PUT api/<apartments>/5
        [HttpPut("{apartmentNum}")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var apart = await _apartmentService.Remove(id);

            if (apart != null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}