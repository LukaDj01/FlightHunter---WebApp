using Microsoft.AspNetCore.Mvc;
using FHLibrary;
using FHLibrary.DTOsCass;
using FHLibrary.DTOsNeo;

namespace FlightHunter.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    [HttpPost]
    [Route("AddFlight/{acEmail}/{pib1}/{pib2}/{serialNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight([FromBody] FlightView f, string acEmail, string pib1, string pib2, string serialNumber)
    {
        var data = await CassandraDataProvider.AddFlight(f, acEmail, pib1, pib2, serialNumber);

        if (data.IsError)
        {
            return BadRequest(data.Error);
        }
        /*var data2 = await CassandraDataProvider.AddFlightAC(f, acEmail, pib1, pib2, serialNumber);
        if (data2.IsError)
        {
            return BadRequest(data2.Error);
        }*/
        return Ok($"Uspešno dodat let. serijski broj: {f.serial_number}");
    }
    
    [HttpPut]
    [Route("UpdateFlightBuyTicket/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFlightBuyTicket(string serial_number)
    {
        (bool IsError, var success, string? error) = await CassandraDataProvider.UpdateFlightBuyTicket(serial_number);

        if (IsError)
        {
            return BadRequest(error);
        }

        if(success)
            return Ok($"Uspešno ažuriran let. serijski broj: {serial_number}");
        else
            return BadRequest("Desila se greska");
    }
    
    /*[HttpGet]
    [Route("GetExpiredFlights")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetExpiredFlight()
    {
        (bool IsError, var expiredFlights, string? error) = await Neo4JDataProvider.GetExpiredFlights();

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(expiredFlights);
    }*/
    
    /*[HttpGet]
    [Route("GetExpiredFlight/{serial_number}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetExpiredFlight(string serial_number)
    {
        (bool IsError, var expiredFlight, string? error) = await Neo4JDataProvider.GetExpiredFlight(serial_number);

        if (IsError)
        {
            return BadRequest(error);
        }

        return Ok(expiredFlight);
    }*/
    
    [HttpDelete]
    [Route("DeleteFlightsOutdated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteFlightsOutdated()
    {
        (bool IsError, var flights, string? error) = await CassandraDataProvider.GetAllFlights();

        if (IsError)
        {
            return BadRequest(error);
        }
        if(flights != null)
            {
                foreach (var flight in flights)
                {
                    if(flight.dateTimeTakeOff.CompareTo(DateTime.Now)<0)
                    {
                    ExpiredFlightView ef = new ExpiredFlightView
                    {
                        serial_number = flight.serial_number,
                        capacity = flight.capacity,
                        available_seats = flight.available_seats,
                        gateTakeOff = flight.gateTakeOff,
                        gateLand = flight.gateLand,
                        dateTimeTakeOff = flight.dateTimeTakeOff,
                        dateTimeLand = flight.dateTimeLand
                    };
                    await Neo4JDataProvider.AddExpiredFlight(ef, flight.avioCompanyEmail!, flight.takeOffAirportPib!, flight.landAirportPib!, flight.planeSerialNumber!);
                        (IsError, var tickets, error) = await CassandraDataProvider.GetTicketsFlight(flight.serial_number!);
                        if (IsError)
                        {
                            return BadRequest(error);
                        }
                        if(tickets!=null)
                        {
                            foreach (var ticket in tickets)
                            {
                                TicketsView t = new TicketsView
                                {
                                    id = ticket.id,
                                    purchaseDate = ticket.purchaseDate,
                                    seatNumber = ticket.seatNumber,
                                    price = ticket.price
                                };
                                await Neo4JDataProvider.AddTicket(t, ticket.passengerEmail!, flight.serial_number!);
                                (IsError, var luggages, error) = await CassandraDataProvider.GetLuggages(ticket.id!);
                                if (IsError)
                                {
                                    return BadRequest(error);
                                }
                                if(luggages!=null)
                                {
                                    foreach (var luggage in luggages)
                                    {
                                        LuggageView l = new LuggageView
                                        {
                                            number = luggage.number,
                                            weight = luggage.weight,
                                            dimension = luggage.dimension,
                                            pricePerKG = luggage.pricePerKG
                                        };
                                        await Neo4JDataProvider.AddLuggage(l, ticket.id!);
                                    }
                                }

                                await CassandraDataProvider.DeleteLuggages(ticket.id!);
                                await CassandraDataProvider.DeletePassTicket(ticket.passengerEmail!, ticket.flightSerialNumber!);
                            }
                        }
                        await CassandraDataProvider.DeleteFlight(flight.serial_number!, flight.avioCompanyEmail!);
                    }

                }
            }


        return Ok($"Uspešno");
    }
}
