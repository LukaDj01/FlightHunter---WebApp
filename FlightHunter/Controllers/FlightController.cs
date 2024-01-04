using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    [HttpPost]
    [Route("AddFlight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] FlightView f)
    {
        var data = await Neo4JDataProvider.AddFlight(f);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodat let. serijski broj: {f.serial_number}");
    }
    
    [HttpPut]
    [Route("UpdateFlight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFlight([FromBody] FlightView f)
    {
        (bool IsError, var flight, string? error) = await Neo4JDataProvider.UpdateFlight(f);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (flight==null)
        {
            return BadRequest("Let nije validan.");
        }

        return Ok($"Uspešno ažuriran let. serijski broj: {flight.serial_number}");
    }
    
    [HttpGet]
    [Route("GetFlight/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFlight(string serial_number)
    {
        (bool IsError, var flight, string? error) = await Neo4JDataProvider.GetFlight(serial_number);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(flight);
    }
    
    [HttpDelete]
    [Route("DeleteFlight/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFlight(string serial_number)
    {
        var data = await Neo4JDataProvider.DeleteFlight(serial_number);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan let. serial_number: {serial_number}");
    }
}
