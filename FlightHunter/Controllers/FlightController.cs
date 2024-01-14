using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsCass;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    [HttpPost]
    [Route("AddFlight/{acEmail}/{pib1}/{pib2}/{serialNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight([FromBody] FlightView f, string acEmail, string pib1, string pib2, string serialNumber)
    {
        var data = await CassandraDataProvider.AddFlight(f, acEmail, pib1, pib2, serialNumber);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }
        /*var data2 = await CassandraDataProvider.AddFlightAC(f, acEmail, pib1, pib2, serialNumber);
        if (data2.IsError)
        {
            return BadRequest(data2.Error);
        }*/
        return Ok($"Uspešno dodat let. serijski broj: {f.serial_number}");
    }
    
    /*[HttpPut]
    [Route("UpdateExpiredFlight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateExpiredFlight([FromBody] ExpiredFlightView f)
    {
        (bool IsError, var expiredFlight, string? error) = await Neo4JDataProvider.UpdateExpiredFlight(f);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (expiredFlight==null)
        {
            return BadRequest("Let nije validan.");
        }

        return Ok($"Uspešno ažuriran let. serijski broj: {expiredFlight.serial_number}");
    }*/
    
    /*[HttpGet]
    [Route("GetExpiredFlights")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetExpiredFlight()
    {
        (bool IsError, var expiredFlights, string? error) = await Neo4JDataProvider.GetExpiredFlights();

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(expiredFlights);
    }*/
    
    /*[HttpGet]
    [Route("GetExpiredFlight/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetExpiredFlight(string serial_number)
    {
        (bool IsError, var expiredFlight, string? error) = await Neo4JDataProvider.GetExpiredFlight(serial_number);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(expiredFlight);
    }*/
    
    /*[HttpDelete]
    [Route("DeleteExpiredFlight/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteExpiredFlight(string serial_number)
    {
        var data = await Neo4JDataProvider.DeleteExpiredFlight(serial_number);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan let. serial_number: {serial_number}");
    }*/
}
