using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
using FlightHunter.Services;
using FHLibrary.DTOsCass;
namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class AvioCompanyController : ControllerBase
{
    [HttpPost]
    [Route("AddAvioCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAvioCompany([FromBody] AvioCompanyView ac)
    {
         if (!EmailValidation.IsEmailValid(ac.email))
        {
            return BadRequest("Nevažeći domen e-pošte. Dozvoljeni su samo domeni poput @gmail, @hotmail, @outlook i slični.");
        }

        var get = await Neo4JDataProvider.GetAvioCompany(ac.email);
        if(get.Data != null)
        {
            return BadRequest("Postoji kompanija sa tim imenom");
        }

        var data = await Neo4JDataProvider.AddAvioCompany(ac);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno dodata avio kompanija. email: {ac.email}");
    }
    
    [HttpPut]
    [Route("UpdateAvioCompany")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAvioCompany([FromBody] AvioCompanyView ac)
    {
        (bool IsError, var avioCompany, string? error) = await Neo4JDataProvider.UpdateAvioCompany(ac);

        if (IsError)
        {
            return BadRequest(error);
        }

        if (avioCompany==null)
        {
            return BadRequest("Avio kompanija nije validna.");
        }

        return Ok($"Uspešno ažurirana avio kompanija. email: {avioCompany.email}");
    }
    
    [HttpGet]
    [Route("GetAvioCompany/{email}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAvioCompany(string email)
    {
        (bool IsError, var avioCompany, string? error) = await Neo4JDataProvider.GetAvioCompany(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var planes, error) = await Neo4JDataProvider.GetPlanesAvioComplany(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var fbs, error) = await Neo4JDataProvider.GetFeedbacksAC(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var expiredFlights, error) = await Neo4JDataProvider.GetExpiredFlightsAC(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var flights, error) = await CassandraDataProvider.GetFlightsAC(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        if(avioCompany!=null)
        {
            avioCompany.planes=planes;
            avioCompany.feedbacks=fbs;
            avioCompany.expiredFlights=expiredFlights;
            if(flights != null)
            {
                foreach (var flight in flights!)
                {
                    FlightView f = new FlightView
                    {
                        serial_number = flight.serial_number,
                        capacity = flight.capacity,
                        available_seats = flight.available_seats,
                        dateTimeLand = flight.dateTimeLand,
                        dateTimeTakeOff = flight.dateTimeTakeOff,
                        gateLand = flight.gateLand,
                        gateTakeOff = flight.gateTakeOff
                    };
                    (IsError, var takeOffAirport, error) = await Neo4JDataProvider.GetAirport(flight.takeOffAirportPib!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    f.takeOffAirport = takeOffAirport;
                    (IsError, var landAirport, error) = await Neo4JDataProvider.GetAirport(flight.landAirportPib!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    f.landAirport = landAirport;
                    (IsError, var plane, error) = await Neo4JDataProvider.GetPlane(flight.planeSerialNumber!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    f.plane = plane;
                    avioCompany.flights!.Add(f);
                }
            }
        }

        return Ok(avioCompany);
    }
    
    [HttpGet]
    [Route("GetAvioCompaniesForRate/{passEmail}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAvioCompaniesForRate(string passEmail)
    {
        (bool IsError, var avioCompanies, string? error) = await Neo4JDataProvider.GetAvioCompaniesForRate(passEmail);
        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(avioCompanies);
    }
    [HttpGet]
    [Route("LoginAvioCompany/{email}/{password}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAvioCompany(string email, string password)
    {
        try
        {
            var result = await Neo4JDataProvider.GetAvioCompany(email);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            if(result.Data == null)
            {
                return NoContent();
            }
            if(result.Data.password != password)
            {
                return NoContent();
            }

            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete]
    [Route("DeleteAvioCompany/{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAvioCompany(string email)
    {
        var data = await Neo4JDataProvider.DeleteAvioCompany(email);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }

        return Ok($"Uspešno obrisana avio kompanija. email: {email}");
    }
}
