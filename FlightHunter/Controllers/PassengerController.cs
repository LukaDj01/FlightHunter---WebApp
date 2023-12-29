using Microsoft.AspNetCore.Mvc;
using FHLibrary;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
    [HttpPost]
    [Route("AddPassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPassenger()
    {
        var data = await Neo4JDataProvider.AddPassanger();

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno");
    }
    
}
