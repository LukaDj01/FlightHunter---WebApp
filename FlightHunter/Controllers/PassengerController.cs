using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsNeo;
using FlightHunter.Services;
using FHLibrary.DTOsCass;
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
        
        (bool IsError, var passenger, string? error) = await Neo4JDataProvider.GetPassenger(email);

        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var tickets, error) = await Neo4JDataProvider.GetTicketsPass(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        (IsError, var ticketsCass, error) = await CassandraDataProvider.GetTicketsPass(email);
        if (IsError)
        {
            return BadRequest(error);
        }

        /*(IsError, var fbs, error) = await Neo4JDataProvider.GetFeedbacksAC(email); // za putnika ako bude potrebe
        if (IsError)
        {
            return BadRequest(error);
        }*/

        if(passenger!=null)
        {
            //passenger.feedbacks=fbs;
            if(tickets != null)
            {
                foreach (var ticket in tickets!)
                {
                    TicketsView t = new TicketsView
                    {
                        id = ticket.id,
                        purchaseDate = ticket.purchaseDate,
                        price = ticket.price,
                        seatNumber = ticket.seatNumber,
                        isExpired = true
                    };
                    (IsError, var expiredFlight, error) = await Neo4JDataProvider.GetExpiredFlightTicket(ticket.id!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    t.flight = expiredFlight;
                    (IsError, var luggages, error) = await Neo4JDataProvider.GetLuggagesTicket(ticket.id!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    t.luggages = luggages;
                    passenger.tickets!.Add(t);
                }
            }

            if(ticketsCass != null)
            {
                foreach (var ticket in ticketsCass!)
                {
                    TicketsView t = new TicketsView
                    {
                        id = ticket.id,
                        purchaseDate = ticket.purchaseDate,
                        price = ticket.price,
                        seatNumber = ticket.seatNumber,
                        isExpired = false
                    };
                    (IsError, var flightCass, error) = await CassandraDataProvider.GetFlight(ticket.flightSerialNumber!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    if(flightCass != null)
                    {
                        ExpiredFlightView f = new ExpiredFlightView
                            {
                                serial_number = flightCass.serial_number,
                                capacity = flightCass.capacity,
                                available_seats = flightCass.available_seats,
                                dateTimeLand = flightCass.dateTimeLand,
                                dateTimeTakeOff = flightCass.dateTimeTakeOff,
                                gateLand = flightCass.gateLand,
                                gateTakeOff = flightCass.gateTakeOff
                            };
                            (IsError, var avioCompany, error) = await Neo4JDataProvider.GetAvioCompany(flightCass.avioCompanyEmail!);
                            if (IsError)
                            {
                                return BadRequest(error);
                            }
                            f.avioCompany = avioCompany;
                            (IsError, var takeOffAirport, error) = await Neo4JDataProvider.GetAirport(flightCass.takeOffAirportPib!);
                            if (IsError)
                            {
                                return BadRequest(error);
                            }
                            f.takeOffAirport = takeOffAirport;
                            (IsError, var landAirport, error) = await Neo4JDataProvider.GetAirport(flightCass.landAirportPib!);
                            if (IsError)
                            {
                                return BadRequest(error);
                            }
                            f.landAirport = landAirport;
                            (IsError, var plane, error) = await Neo4JDataProvider.GetPlane(flightCass.planeSerialNumber!);
                            if (IsError)
                            {
                                return BadRequest(error);
                            }
                            f.plane = plane;
                            t.flight = f;
                    }
                    
                    (IsError, var luggagesCass, error) = await CassandraDataProvider.GetLuggages(ticket.id!);
                    if (IsError)
                    {
                        return BadRequest(error);
                    }
                    if(luggagesCass != null)
                    {
                        foreach (var luggageCass in luggagesCass!)
                        {
                            LuggageView l = new LuggageView
                            {
                                number = luggageCass.number,
                                weight = luggageCass.weight,
                                dimension = luggageCass.dimension,
                                pricePerKG = luggageCass.pricePerKG
                            };
                            t.luggages!.Add(l);
                        }
                    }
                    passenger.tickets!.Add(t);
                }
            }
        }


        return Ok(passenger);
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
