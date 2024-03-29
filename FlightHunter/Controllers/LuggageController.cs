﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    [Route("GetLuggage/{ticketID}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLuggage(string ticketID)
    {
        (bool IsError, var luggages, string? error) = await Neo4JDataProvider.GetLuggage(ticketID);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(luggages);
    }

    [HttpDelete]
    [Route("DeleteTLuggages/{ticketID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteTLuggageRel(string ticketID)
    {
        var data = await Neo4JDataProvider.DeleteLuggage(ticketID);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno");
    }
}
