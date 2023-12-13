using Business;
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
        [ProducesResponseType(200)]
        [ProducesErrorResponseType(typeof(ApplicationException))]
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
        [ProducesResponseType(200)]
        [ProducesErrorResponseType(typeof(ApplicationException))]
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

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesErrorResponseType(typeof(ApplicationException))]
        public IActionResult Create([FromBody] Protocol protocol)
        {
            try
            {
                _business.Create(protocol);
                return Ok();
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
