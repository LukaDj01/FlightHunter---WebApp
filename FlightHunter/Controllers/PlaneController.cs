using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]

public class PlaneController : ControllerBase
{
    /*[HttpPost]
    [Route("AddPlane")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPlane([FromBody] PlaneView planeView)
    {
        if (planeView == null)
        {
            return BadRequest("Invalid input data");
        }

        var result = await Neo4JDataProvider.AddPlane(planeView);

        if (result.IsError)
        {
            return BadRequest(result.Error);
        }

        return Ok("Uspešno");
    }*/
}
