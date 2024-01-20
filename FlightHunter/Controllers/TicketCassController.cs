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
    public async Task<IActionResult> AddTicket([FromBody] TicketsCassView ticketView, string passenger_email, string flightSerialNumber)
    {
        if (ticketView == null)
        {
            return BadRequest("Invalid input data");
        }
        ticketView.id=Guid.NewGuid().ToString("N");

        var result = await CassandraDataProvider.AddTicket(ticketView, passenger_email, flightSerialNumber);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok(ticketView.id);
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
    }

    [HttpDelete]
    [Route("DeletePassTicket/{email}/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePassTicketRel(string email, string serial_number)
    {
        var data = await CassandraDataProvider.DeletePassTicket(email, serial_number);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno");
    }

}