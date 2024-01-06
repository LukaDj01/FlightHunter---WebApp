using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    [HttpPost]
    [Route("AddTicket/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddTicket([FromBody] TicketsView ticketView, string id)
    {
        if (ticketView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddTicket(ticketView, id);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }

    [HttpPut]
    [Route("UpdateTicket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTicket([FromBody] TicketsView tickets)
    {
        (bool IsError, var ticket, string? error) = await Neo4JDataProvider.UpdateTicket(tickets);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (ticket == null)
        {
            return BadRequest("Karta nije validna.");
        }

        return Ok($"Uspešno ažurirana karta. Id: {ticket.id}");
    }

    [HttpGet]
    [Route("GetTicket/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAvioCompany(string id)
    {
        (bool IsError, var ticket, string? error) = await Neo4JDataProvider.GetTickets(id);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(ticket);
    }

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
    }

}