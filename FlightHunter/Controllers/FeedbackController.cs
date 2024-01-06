using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedbackController : ControllerBase
{
    [HttpPost]
    [Route("AddFeedbackPassAC/{passEmail}/{acEmail}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFeedbackPassAC([FromBody] FeedbackView fb, string passEmail, string acEmail)
    {
        var data = await Neo4JDataProvider.AddFeedbackPassAC(fb, passEmail, acEmail);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodata recenzija. id: {fb.id}");
    }
    [HttpPost]
    [Route("AddFeedbackPassAirport/{passEmail}/{airportPib}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFeedbackPassAirport([FromBody] FeedbackView fb, string passEmail, string airportPib)
    {
        var data = await Neo4JDataProvider.AddFeedbackPassAirport(fb, passEmail, airportPib);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodata recenzija. id: {fb.id}");
    }
    
    /*[HttpPut]
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
    }*/
    
    [HttpGet]
    [Route("GetFeedbacksAC/{acEmail}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFeedbacksAC(string acEmail)
    {
        (bool IsError, var feed, string? error) = await Neo4JDataProvider.GetFeedbacksAC(acEmail);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(feed);
    }
    
    [HttpGet]
    [Route("GetFeedbacksAirport/{airportPib}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFeedbacksAirport(string airportPib)
    {
        (bool IsError, var feed, string? error) = await Neo4JDataProvider.GetFeedbacksAirport(airportPib);

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
