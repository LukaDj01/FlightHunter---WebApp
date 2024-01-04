using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
    //add
    [HttpPost]
    [Route("AddPassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPassenger([FromBody] PassengerView p)
    {
        var data = await Neo4JDataProvider.AddPassenger(p);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodat putnik. email: {p.email}");
    }
    //update
    [HttpPut]
    [Route("UpdatePassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassenger([FromBody] PassengerView p)
    {
        (bool IsError, var passenger, string? error) = await Neo4JDataProvider.UpdatePassenger(p);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (passenger==null)
        {
            return BadRequest("Putnik nije validan.");
        }

        return Ok($"Uspešno ažuriran putnik. email: {passenger.email}");
    }
    //get
    [HttpGet]
    [Route("GetPassenger/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPassenger(string email)
    {
        (bool IsError, var pass, string? error) = await Neo4JDataProvider.GetPassenger(email);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(pass);
    }
    //delete
    [HttpDelete]
    [Route("DeletePassenger/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePassenger(string email)
    {
        var data = await Neo4JDataProvider.DeletePassenger(email);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan putnik. email: {email}");
    }
}
