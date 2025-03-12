using Apartments.Core.DTOs;
using Apartments.Core.Services;
using Apartments.Data;
using Apartments.Entitise;
using Apartments.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class brokers : ControllerBase
    {
        private readonly IBrokerService _brokerService;
        private readonly IMapper _Mapper;

        public brokers(IBrokerService brokerService,IMapper map)
        {
            _brokerService = brokerService;
            _Mapper = map;
        }
        // GET: api/<brokers>
        [HttpGet]
        [AllowAnonymous]
        public async Task <ActionResult> Get()
        {
            var brokerList = await _brokerService.GetAll();
            var brokers = _Mapper.Map<IEnumerable<BrokerDto>>(brokerList);
            return Ok(brokers);
        }

        // GET api/<brokers>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task <ActionResult> GetById(int id)
        {
            var bro = await _brokerService.GetById(id);
            if (bro != null)
            {
                return Ok(bro);  
            }
            return NotFound();  
        }

        // POST api/<brokers>
        [HttpPost]
        [Authorize(Roles = "Broker")]
        public async Task <ActionResult> Post([FromBody] BrokerPostModel b)
        {
            var newbroker = _Mapper.Map<Broker>(b);
            await _brokerService.Add(newbroker);
            return Ok();

        }
        // PUT api/<brokers>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Broker")]
        public async Task <ActionResult> Put(int id, [FromBody] BrokerPostModel b)
        {
            var bro = await _brokerService.Put(id, _Mapper.Map<Broker>(b));
            if (bro != null)
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/<brokers>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Broker")]
        public async Task <ActionResult> Delete(int id)
        {
            var bro = await _brokerService.Remove(id);
            if (bro != null)
            {
                return Ok();
            }
            return NotFound();
        }  
    }
}
