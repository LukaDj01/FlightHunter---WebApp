using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]

public class LuggageController : ControllerBase
{
    [HttpPost]
    [Route("AddLuggage/{tId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddLuggage([FromBody] LuggageView luggageView, string tId)
    {
        if (luggageView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddLuggage(luggageView, tId);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }

    [HttpPut]
    [Route("UpdateLuggage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLuggage([FromBody] LuggageView luggages)
    {
        (bool IsError, var luggage, string? error) = await Neo4JDataProvider.UpdateLuggage(luggages);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (luggage == null)
        {
            return BadRequest("Prtljag nije validan.");
        }

        return Ok($"Uspešno ažuriran prtljag. Id: {luggage.number}");
    }

    [HttpGet]
    [Route("GetLuggage/{number}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLuggage(string number)
    {
        (bool IsError, var luggage, string? error) = await Neo4JDataProvider.GetLuggage(number);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(luggage);
    }

    [HttpDelete]
    [Route("DeleteTLuggageRel/{numberLuggage}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTLuggageRel(string numberLuggage)
    {
        var data = await Neo4JDataProvider.DeleteTLuggageRel(numberLuggage);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisana veza karte.  Broj: {numberLuggage}");
    }
}
