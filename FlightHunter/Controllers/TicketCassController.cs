using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketCassController : ControllerBase
{
    [HttpPost]
    [Route("AddTicket/{passenger_email}/{flightSerialNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddTicket([FromBody] TicketsCassView ticketView)
    {
        if (ticketView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await CassandraDataProvider.AddTicket(ticketView);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok($"Uspešno dodata karta. ID karte: {ticketView.id}");
    }

    [HttpGet]
    [Route("GetTicketsPass/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTicketsPass(string email)
    {
        (bool IsError, var tickets, string? error) = await CassandraDataProvider.GetTicketsPass(email);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(tickets);
    }/*

    [HttpDelete]
    [Route("DeletePassTicketRel/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePassTicketRel(string id)
    {
        var data = await Neo4JDataProvider.DeleteTickets(id);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisana karta. Id: {id}");
    }*/

}