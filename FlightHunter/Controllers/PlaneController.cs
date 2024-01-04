using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]

public class PlaneController : ControllerBase
{
    [HttpPost]
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
    }

    [HttpPut]
    [Route("UpdatePlane")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePlane([FromBody] PlaneView planes)
    {
        (bool IsError, var plane, string? error) = await Neo4JDataProvider.UpdatePlane(planes);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (plane == null)
        {
            return BadRequest("Avion nije validan.");
        }

        return Ok($"Uspešno ažuriran avion. Serijski broj: {plane.serialNumber}");
    }

    [HttpGet]
    [Route("GetPlane/{serialNumber}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlane(string serialNumber)
    {
        (bool IsError, var plane, string? error) = await Neo4JDataProvider.GetPlane(serialNumber);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(plane);
    }

    [HttpDelete]
    [Route("DeletePlane/{serialNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePlane(string serialNumber)
    {
        var data = await Neo4JDataProvider.DeletePlane(serialNumber);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisan avion. Serijski broj: {serialNumber}");
    }
}
