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

    [HttpPut]
    [Route("UpdateAirport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAirport([FromBody] AirportView a)
    {
        (bool IsError, var airport, string? error) = await Neo4JDataProvider.UpdateAirport(a);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (airport == null)
        {
            return BadRequest("Aerodrom nije validna.");
        }

        return Ok($"Uspešno ažurirana aerodrom.");
    }

    [HttpGet]
    [Route("GetAirport/{pib}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAirport(string pib)
    {
        (bool IsError, var airport, string? error) = await Neo4JDataProvider.GetAirport(pib);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(airport);
    }

    [HttpDelete]
    [Route("DeleteAirport/{pib}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAirport(string pib)
    {
        var data = await Neo4JDataProvider.DeleteAirport(pib);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan aerodrom. Pib: {pib}");
    }
}