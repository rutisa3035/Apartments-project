using Apartments.Core.Services;
using Apartments.Entitise;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apartments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class brokers : ControllerBase
    {

        private readonly IBrokerService _brokerService;
        public brokers(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        // GET: api/<brokers>
        [HttpGet]

        
        public ActionResult Get()
        {
         return Ok(_Context.Broker);

        }

        // GET api/<brokers>/5
        [HttpGet("{id}")]
     
        public ActionResult Getid(int id)
        {
            var broker = _Context.Broker.Find(b => b.Id == id);
            if (broker == null)
            {
                return NotFound();  
            }
            return Ok(broker);  
        }

        // POST api/<brokers>
        [HttpPost]
        public ActionResult Post([FromBody] Broker value)
        {
            _Context.Broker.Add(value);
            return Ok(value);
        }

        // PUT api/<brokers>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Broker value)
        {
            var index = _Context.Broker.FindIndex(e => e.Id == id);
            _Context.Broker[index].Name = value.Name;
            _Context.Broker[index].Phone_number = value.Phone_number;
            _Context.Broker[index].Email = value.Email;
            _Context.Broker[index].Adress = value.Adress;
            return Ok(_Context.Broker[index]);

        }

        // DELETE api/<brokers>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
