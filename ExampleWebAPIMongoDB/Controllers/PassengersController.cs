using ExampleWebAPIMongoDB.Models;
using ExampleWebAPIMongoDB.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleWebAPIMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {

        private readonly PassengerService _passengerService;

        public PassengersController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassenger()
        {
            return Ok(await _passengerService.GetPassengersAsync());
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Passenger>> GetPassenger(string cpf)
        {
            var passageiro = await _passengerService.GetPassengerByIdAsync(cpf);

            if (passageiro == null)
            {
                return NotFound();
            }

            return passageiro;
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutPassageiro(string cpf, Passenger passageiro)
        {
            if (cpf != passageiro.Cpf)
            {
                return BadRequest();
            }

            await _passengerService.UpdateAsync(cpf, passageiro);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassageiro(Passenger passageiro)
        {

            await _passengerService.AddAsync(passageiro);

            return CreatedAtAction("GetPassenger", new { id = passageiro.Cpf }, passageiro);
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> DeletePassageiro(string cpf)
        {
            var passageiro = await GetPassenger(cpf);
            if (passageiro == null)
            {
                return NotFound();
            }
            await _passengerService.RemoveAsync(cpf);

            return NoContent();
        }

    }
}
