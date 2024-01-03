using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class AirportController : ControllerBase
{
    [HttpPost]
    [Route("AddAirport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAirport([FromBody] AirportView airportView)
    {
        if (airportView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddAirport(airportView);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }
}