using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]

public class LuggageController : ControllerBase
{
    [HttpPost]
    [Route("AddLuggage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddLuggage([FromBody] LuggageView luggageView)
    {
        if (luggageView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddLuggage(luggageView);

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
    [Route("DeleteLuggage/{number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteLuggage(string number)
    {
        var data = await Neo4JDataProvider.DeleteLuggage(number);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan prtljag. Broj prtljaga: {number}");
    }

}
