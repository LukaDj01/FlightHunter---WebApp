using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
using FlightHunter.Services;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController : ControllerBase
{
    //add
    [HttpPost]
    [Route("AddPassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPassenger([FromBody] PassengerView p)
    {
        try
        {
            if (!EmailValidation.IsEmailValid(p.email))
            {
                return BadRequest("Nevažeći domen e-pošte. Dozvoljeni su samo domeni poput @gmail, @hotmail, @outlook i slični.");
            }
            var get = await Neo4JDataProvider.GetPassenger(p.email);
            if(get.Data != null)
            {
                return BadRequest("Postoji putnik sa tim imenom");
            }

            var data = await Neo4JDataProvider.AddPassenger(p);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }
            return Ok($"Uspešno dodat putnik. email: {p.email}");

        }
        catch(Exception e)
        {
            return StatusCode(500, "Internal Server Error");
        }
 
    }
    /*//update
    [HttpPut]
    [Route("UpdatePassenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassenger([FromBody] PassengerView p)
    {
        (bool IsError, var passenger, string? error) = await Neo4JDataProvider.UpdatePassenger(p);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (passenger==null)
        {
            return BadRequest("Putnik nije validan.");
        }

        return Ok($"Uspešno ažuriran putnik. email: {passenger.email}");
    }*/
    //get
    [HttpGet]
    [Route("GetPassenger/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPassenger(string email)
    {
        /*(bool IsError, var pass, string? error) = await Neo4JDataProvider.GetPassenger(email);

        if (IsError)
        {
            return BadRequest(error); //"Putnik sa zadatim e-mailom ne postoji u bazi!"
        }

        return Ok(pass);*/
        try
        {
            var result = await Neo4JDataProvider.GetPassenger(email);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet]
    [Route("LoginPassenger/{email}/{password}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginPassenger(string email, string password)
    {
        try
        {
            var result = await Neo4JDataProvider.GetPassenger(email);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            if(result.Data == null)
            {
                return BadRequest("Putnik ne postoji u bazi! Bezuspesno logovanje!");
            }
            if(result.Data.password != password)
            {
                return BadRequest("Lozinke se ne poklapaju");
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

   [HttpGet]
    [Route("GetPassengers")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPassengers()
    {
        (bool IsError, var pass, string? error) = await Neo4JDataProvider.GetPassenger();

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(pass);
    }
    //delete
    [HttpDelete]
    [Route("DeletePassenger/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePassenger(string email)
    {
         try
        {
            var data = await Neo4JDataProvider.DeletePassenger(email);
            if (data.IsError)
            {
                return BadRequest(data.Error);
            }
            else
            {
                if(data.Data)
                {                
                    return Ok($"Uspešno obrisan putnik sa e-mailom; email: {email}");
                }
                else
                {
                    return BadRequest("Pokusavate da obrisete putnika koji ne postoji u bazi!");
                }
            }

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}
