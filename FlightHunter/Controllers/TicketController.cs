using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    [HttpPost]
    [Route("AddTicket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddTicket([FromBody] TicketsView ticketView)
    {
        if (ticketView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddTicket(ticketView);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }

}