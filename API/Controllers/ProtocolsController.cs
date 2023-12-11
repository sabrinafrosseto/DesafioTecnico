using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtocolsController : ControllerBase
    {
        private readonly IProtocolBusiness _business;

        public ProtocolsController(IProtocolBusiness business)
        {
            _business = business;
        }

        // GET: api/<ProtocolsController>
        [HttpGet]
        public List<Protocol> GetAll()
        {
            try
            {
                return _business.Find();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET api/<ProtocolsController>/5
        [HttpGet("filter")]
        public IActionResult Get([FromQuery] string? protocolNumber, [FromQuery] string? cpf, [FromQuery] string? rg)
        {
            try
            {
                return Ok(_business.Find(protocol: protocolNumber, cpf: cpf, rg: rg));
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                    return BadRequest(ex.Message);

                return StatusCode(500);
            }
        }

        // POST api/<ProtocolsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProtocolsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProtocolsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
