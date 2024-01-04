using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase
{
    [HttpPost]
    [Route("AddFeedback")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFeedback([FromBody] FeedbackView fb)
    {
        var data = await Neo4JDataProvider.AddFeedback(fb);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodata recenzija. id: {fb.id}");
    }
    
    [HttpPut]
    [Route("UpdateFeedback")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackView fb)
    {
        (bool IsError, var feed, string? error) = await Neo4JDataProvider.UpdateFeedback(fb);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (feed==null)
        {
            return BadRequest("Recenzija nije validna.");
        }

        return Ok($"Uspešno ažurirana recenzija. id: {feed.id}");
    }
    
    [HttpGet]
    [Route("GetFeedback/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFeedback(string id)
    {
        (bool IsError, var feed, string? error) = await Neo4JDataProvider.GetFeedback(id);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(feed);
    }
    
    [HttpDelete]
    [Route("DeleteFeedback/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFeedback(string id)
    {
        var data = await Neo4JDataProvider.DeleteFeedback(id);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisana recenzija. id: {id}");
    }
}
