using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]

public class LuggageCassController : ControllerBase
{
    [HttpPost]
    [Route("AddLuggage/{tId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddLuggage([FromBody] LuggageCassView luggageView, string tId)
    {
        if (luggageView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await CassandraDataProvider.AddLuggage(luggageView, tId);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }

    [HttpGet]
    [Route("GetLuggage/{ticketID}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLuggage(string ticketID)
    {
        (bool IsError, var luggages, string? error) = await CassandraDataProvider.GetLuggages(ticketID);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(luggages);
    }

    [HttpDelete]
    [Route("DeleteLuggages/{ticketID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLuggages(string ticketID)
    {
        var data = await CassandraDataProvider.DeleteLuggages(ticketID);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno");
    }
}
