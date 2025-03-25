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
using static Apartments.Core.Entitise.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class brokers : ControllerBase
    {
        private readonly IBrokerService _brokerService;
        private readonly IMapper _Mapper;
        private readonly IUsersService _usersService;

        public brokers(IBrokerService brokerService,IMapper map, IUsersService usersService)
        {
            _brokerService = brokerService;
            _Mapper = map;
            _usersService = usersService;

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
        public async Task <ActionResult> Post([FromBody] BrokerPostModel b)
        {
            var user = new  Users { UserName = b.UserName, Password = b.Password, Role = eRole.manager };
            var User = await _usersService.AddUserAsync(user);
            var newbroker = _Mapper.Map<Broker>(b);
            newbroker.user = User;
            newbroker.UserId = User.Id;
            var broker = await _brokerService.GetById(newbroker.Id);
               if (broker != null)
               {
                   return Conflict();
               }
            await _brokerService.Add(newbroker);
            return Ok();

        }


        // PUT api/<brokers>/5
        [HttpPut("{id}")]
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
