using Cassandra;
using FHLibrary.DTOsCass;

namespace FHLibrary;
public static class CassandraDataProvider
{
    public static string generateID()
    {
        return Guid.NewGuid().ToString("N"); // N - da bi dobili string bez crtica izmedju heksadecimalnih brojeva
    }

    #region Flight
    

    
    public async static Task<Result<List<FlightInput>, string>> GetAllFlights()
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<FlightInput> flights = new List<FlightInput>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            List<Row>? flightsData = s.Execute("select * from \"Flight\"").ToList();
            
            foreach (var flightData in flightsData)
            {
                if(flightData != null)
                {
                    FlightInput flight = new FlightInput
                    {
                        serial_number = flightData["serial_number"] != null ? flightData["serial_number"].ToString() : string.Empty,
                        capacity = flightData["capacity"] != null ? int.Parse(flightData["capacity"].ToString()!) : 0,
                        available_seats = flightData["available_seats"] != null ? int.Parse(flightData["available_seats"].ToString()!) : 0,
                        avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString() : string.Empty,
                        takeOffAirportPib = flightData["takeOffAirportPib"] != null ? flightData["takeOffAirportPib"].ToString() : string.Empty,
                        landAirportPib = flightData["landAirportPib"] != null ? flightData["landAirportPib"].ToString() : string.Empty,
                        planeSerialNumber = flightData["planeSerialNumber"] != null ? flightData["planeSerialNumber"].ToString() : string.Empty,
                        dateTimeLand = flightData["dateTimeLand"] != null ? DateTime.Parse(flightData["dateTimeLand"].ToString()!) : new DateTime(),
                        dateTimeTakeOff = flightData["dateTimeTakeOff"] != null ? DateTime.Parse(flightData["dateTimeTakeOff"].ToString()!) : new DateTime(),
                        gateLand = flightData["gateLand"] != null ? flightData["gateLand"].ToString() : string.Empty,
                        gateTakeOff = flightData["gateTakeOff"] != null ? flightData["gateTakeOff"].ToString() : string.Empty
                    };
                    flights.Add(flight);
                }
            }
            
            return flights;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<FlightInput, string>> GetFlight(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            FlightInput flight = new FlightInput();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            Row? flightData = s.Execute("select * from \"Flight\" where serial_number = '" + serial_number + "'").FirstOrDefault();

            if(flightData != null)
            {
                flight.serial_number = flightData["serial_number"] != null ? flightData["serial_number"].ToString() : string.Empty;
                flight.capacity = flightData["capacity"] != null ? int.Parse(flightData["capacity"].ToString()!) : 0;
                flight.available_seats = flightData["available_seats"] != null ? int.Parse(flightData["available_seats"].ToString()!) : 0;
                flight.avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString() : string.Empty;
                flight.takeOffAirportPib = flightData["takeOffAirportPib"] != null ? flightData["takeOffAirportPib"].ToString() : string.Empty;
                flight.landAirportPib = flightData["landAirportPib"] != null ? flightData["landAirportPib"].ToString() : string.Empty;
                flight.planeSerialNumber = flightData["planeSerialNumber"] != null ? flightData["planeSerialNumber"].ToString() : string.Empty;
                flight.dateTimeLand = flightData["dateTimeLand"] != null ? DateTime.Parse(flightData["dateTimeLand"].ToString()!) : new DateTime();
                flight.dateTimeTakeOff = flightData["dateTimeTakeOff"] != null ? DateTime.Parse(flightData["dateTimeTakeOff"].ToString()!) : new DateTime();
                flight.gateLand = flightData["gateLand"] != null ? flightData["gateLand"].ToString() : string.Empty;
                flight.gateTakeOff = flightData["gateTakeOff"] != null ? flightData["gateTakeOff"].ToString() : string.Empty;
            }
            
            return flight;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    
    public async static Task<Result<List<FlightInput>, string>> GetFlightsAC(string email)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<FlightInput> flights = new List<FlightInput>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            List<Row>? flightsData = s.Execute("select * from \"FlightAC\" where \"avioCompanyEmail\" = '" + email + "'").ToList();
            
            foreach (var flightData in flightsData)
            {
                if(flightData != null)
                {
                    FlightInput flight = new FlightInput
                    {
                        serial_number = flightData["serial_number"] != null ? flightData["serial_number"].ToString() : string.Empty,
                        capacity = flightData["capacity"] != null ? int.Parse(flightData["capacity"].ToString()!) : 0,
                        available_seats = flightData["available_seats"] != null ? int.Parse(flightData["available_seats"].ToString()!) : 0,
                        avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString() : string.Empty,
                        takeOffAirportPib = flightData["takeOffAirportPib"] != null ? flightData["takeOffAirportPib"].ToString() : string.Empty,
                        landAirportPib = flightData["landAirportPib"] != null ? flightData["landAirportPib"].ToString() : string.Empty,
                        planeSerialNumber = flightData["planeSerialNumber"] != null ? flightData["planeSerialNumber"].ToString() : string.Empty,
                        dateTimeLand = flightData["dateTimeLand"] != null ? DateTime.Parse(flightData["dateTimeLand"].ToString()!) : new DateTime(),
                        dateTimeTakeOff = flightData["dateTimeTakeOff"] != null ? DateTime.Parse(flightData["dateTimeTakeOff"].ToString()!) : new DateTime(),
                        gateLand = flightData["gateLand"] != null ? flightData["gateLand"].ToString() : string.Empty,
                        gateTakeOff = flightData["gateTakeOff"] != null ? flightData["gateTakeOff"].ToString() : string.Empty
                    };
                    flights.Add(flight);
                }
            }
            
            return flights;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    
    public async static Task<Result<List<FlightInput>, string>> GetFlightsSearch(string pib1, string pib2, string emailAC, string date)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<FlightInput> flights = new List<FlightInput>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            var query = "select * from \"SearchFlight\"";
            if(pib1!="" && pib1!=null && pib1!="o")
            {
                query = "select * from \"SearchFlight\" where \"takeOffAirportPib\" = '" + pib1 + "'";
                if(pib2!="" && pib2!=null && pib2!="o")
                {
                    query = "select * from \"SearchFlight\" where \"takeOffAirportPib\" = '" + pib1 + "' and \"landAirportPib\" = '" + pib2 + "'";
                    if(emailAC!="" && emailAC!=null && emailAC!="o")
                    {
                        query = "select * from \"SearchFlight\" where \"takeOffAirportPib\" = '" + pib1 + "' and \"landAirportPib\" = '" + pib2 + "' and \"avioCompanyEmail\" = '" + emailAC + "'";
                        if(date!="" && date!=null && date!="o")
                        {
                            query = "select * from \"SearchFlight\" where \"takeOffAirportPib\" = '" + pib1 + "' and \"landAirportPib\" = '" + pib2 + "' and \"avioCompanyEmail\" = '" + emailAC + "' and \"dateTimeTakeOff\" = '" + DateTime.Parse(date) + "'";
                                
                        }    
                    }
                }
            }

            List<Row>? flightsData = s.Execute(query).ToList();
            
            foreach (var flightData in flightsData)
            {
                if(flightData != null)
                {
                    FlightInput flight = new FlightInput
                    {
                        serial_number = flightData["flightSerialNumber"] != null ? flightData["flightSerialNumber"].ToString() : string.Empty,
                        avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString() : string.Empty,
                        takeOffAirportPib = flightData["takeOffAirportPib"] != null ? flightData["takeOffAirportPib"].ToString() : string.Empty,
                        landAirportPib = flightData["landAirportPib"] != null ? flightData["landAirportPib"].ToString() : string.Empty,
                        dateTimeTakeOff = flightData["dateTimeTakeOff"] != null ? DateTime.Parse(flightData["dateTimeTakeOff"].ToString()!) : new DateTime(),
                    };
                    flights.Add(flight);
                }
            }
            
            return flights;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<bool, string>> UpdateFlightBuyTicket(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            FlightInput flight = new FlightInput();
            Row? flightData = s.Execute("select * from \"Flight\" where serial_number = '" + serial_number + "'").FirstOrDefault();

            if(flightData != null)
            {
                flight.capacity = flightData["capacity"] != null ? int.Parse(flightData["capacity"].ToString()!) : 0;
                flight.available_seats = flightData["available_seats"] != null ? int.Parse(flightData["available_seats"].ToString()!) : 0;
                flight.avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString()! : string.Empty;
            }
            else return false;

            if(flight.available_seats > 0)
            {
                flight.available_seats--;
                s.Execute("update \"Flight\" set available_seats='" + flight.available_seats + "' where serial_number = '" + serial_number + "'");
                s.Execute("update \"FlightAC\" set available_seats='" + flight.available_seats + "' where \"avioCompanyEmail\" = '" + flight.avioCompanyEmail + "'");
                return true;
            }
            
            return false;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<bool, string>> AddFlight(FlightView f, string acEmail, string pib1, string pib2, string serialNumber)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            f.serial_number = generateID();

            s.Execute("insert into \"Flight\" (serial_number, capacity, available_seats, \"avioCompanyEmail\", \"landAirportPib\", \"takeOffAirportPib\", \"planeSerialNumber\", \"dateTimeLand\", \"dateTimeTakeOff\", \"gateLand\", \"gateTakeOff\")  values ('" + f.serial_number +"', " + f.capacity +"," + f.available_seats +",'" + acEmail +"','" + pib2 +"','" + pib1 +"','" + serialNumber +"','" + f.dateTimeLand +"','" + f.dateTimeTakeOff +"','" + f.gateLand +"','" + f.gateTakeOff +"')");
            s.Execute("insert into \"FlightAC\" (\"avioCompanyEmail\", serial_number, capacity, available_seats, \"landAirportPib\", \"takeOffAirportPib\", \"planeSerialNumber\", \"dateTimeLand\", \"dateTimeTakeOff\", \"gateLand\", \"gateTakeOff\")  values ('" + acEmail +"','" + f.serial_number +"', " + f.capacity +"," + f.available_seats +",'" + pib2 +"','" + pib1 +"','" + serialNumber +"','" + f.dateTimeLand +"','" + f.dateTimeTakeOff +"','" + f.gateLand +"','" + f.gateTakeOff +"')");
            s.Execute("insert into \"SearchFlight\" (\"takeOffAirportPib\", \"landAirportPib\", \"avioCompanyEmail\", \"dateTimeTakeOff\", \"flightSerialNumber\") values ('" + pib1 +"','" + pib2 +"', '" + acEmail +"','" + f.dateTimeTakeOff +"','" + f.serial_number +"')");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }
    public async static Task<Result<bool, string>> DeleteFlight(string serial_number, string avioCompanyEmail)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            
            FlightInput flight = new FlightInput();
            Row? flightData = s.Execute("select * from \"Flight\" where serial_number = '" + serial_number + "'").FirstOrDefault();

            if(flightData != null)
            {
                flight.serial_number = flightData["serial_number"] != null ? flightData["serial_number"].ToString() : string.Empty;
                flight.avioCompanyEmail = flightData["avioCompanyEmail"] != null ? flightData["avioCompanyEmail"].ToString() : string.Empty;
                flight.takeOffAirportPib = flightData["takeOffAirportPib"] != null ? flightData["takeOffAirportPib"].ToString() : string.Empty;
                flight.landAirportPib = flightData["landAirportPib"] != null ? flightData["landAirportPib"].ToString() : string.Empty;
                flight.dateTimeTakeOff = flightData["dateTimeTakeOff"] != null ? DateTime.Parse(flightData["dateTimeTakeOff"].ToString()!) : new DateTime();
            }

            s.Execute("delete from \"Flight\" where serial_number = '" + serial_number + "'");
            s.Execute("delete from \"FlightAC\" where \"avioCompanyEmail\" = '" + avioCompanyEmail + "' and serial_number = '" + serial_number + "'");
            s.Execute("delete from \"SearchFlight\" where \"takeOffAirportPib\" = '" + flight.takeOffAirportPib + "' and \"landAirportPib\" = '" + flight.landAirportPib + "' and \"avioCompanyEmail\" = '" + flight.avioCompanyEmail + "' and \"dateTimeTakeOff\" = '" + flight.dateTimeTakeOff + "' and \"flightSerialNumber\" = '" + flight.serial_number + "'");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    #endregion
    #region Ticket

    public async static Task<Result<bool, string>> AddTicket(TicketsCassView t, string passenger_email, string flightSerialNumber)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            s.Execute("insert into \"TicketPass\" (\"passengerEmail\", \"flightSerialNumber\", \"purchaseDate\", id, price, \"seatNumber\")  values ('" + passenger_email +"', '" + flightSerialNumber +"','" + t.purchaseDate +"','" + t.id +"'," + t.price +",'" + t.seatNumber +"')");
            s.Execute("insert into \"TicketFlight\" (\"flightSerialNumber\", \"passengerEmail\", \"purchaseDate\", id, price, \"seatNumber\")  values ('" + flightSerialNumber +"', '" + passenger_email +"','" + t.purchaseDate +"','" + t.id +"'," + t.price +",'" + t.seatNumber +"')");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    public async static Task<Result<List<TicketsCassView>, string>> GetTicketsPass(string email)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<TicketsCassView> tickets = new List<TicketsCassView>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            List<Row>? ticketsData = s.Execute("select * from \"TicketPass\" where \"passengerEmail\" = '" + email + "'").ToList();
            
            foreach (var ticketData in ticketsData)
            {
                if(ticketData != null)
                {
                    TicketsCassView ticket = new TicketsCassView
                    {
                        passengerEmail = ticketData["passengerEmail"] != null ? ticketData["passengerEmail"].ToString() : string.Empty,
                        flightSerialNumber = ticketData["flightSerialNumber"] != null ? ticketData["flightSerialNumber"].ToString() : string.Empty,
                        seatNumber = ticketData["seatNumber"] != null ? ticketData["seatNumber"].ToString() : string.Empty,
                        purchaseDate = ticketData["purchaseDate"] != null ? DateTime.Parse(ticketData["purchaseDate"].ToString()!) : new DateTime(),
                        id = ticketData["id"] != null ? ticketData["id"].ToString() : string.Empty,
                        price = ticketData["price"] != null ? float.Parse(ticketData["price"].ToString()!) : 0,
                        isExpired = false
                    };
                    tickets.Add(ticket);
                }
            }
            
            return tickets;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    public async static Task<Result<List<TicketsCassView>, string>> GetTicketsFlight(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<TicketsCassView> tickets = new List<TicketsCassView>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            List<Row>? ticketsData = s.Execute("select * from \"TicketFlight\" where \"flightSerialNumber\" = '" + serial_number + "'").ToList();
            
            foreach (var ticketData in ticketsData)
            {
                if(ticketData != null)
                {
                    TicketsCassView ticket = new TicketsCassView
                    {
                        flightSerialNumber = ticketData["flightSerialNumber"] != null ? ticketData["flightSerialNumber"].ToString() : string.Empty,
                        passengerEmail = ticketData["passengerEmail"] != null ? ticketData["passengerEmail"].ToString() : string.Empty,
                        seatNumber = ticketData["seatNumber"] != null ? ticketData["seatNumber"].ToString() : string.Empty,
                        purchaseDate = ticketData["purchaseDate"] != null ? DateTime.Parse(ticketData["purchaseDate"].ToString()!) : new DateTime(),
                        id = ticketData["id"] != null ? ticketData["id"].ToString() : string.Empty,
                        price = ticketData["price"] != null ? float.Parse(ticketData["price"].ToString()!) : 0,
                        isExpired = false
                    };
                    tickets.Add(ticket);
                }
            }
            
            return tickets;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<bool, string>> DeletePassTicket(string email, string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            s.Execute("delete from \"TicketPass\" where \"passengerEmail\" = '" + email + "' and \"flightSerialNumber\" = '" + serial_number + "'");
            s.Execute("delete from \"TicketFlight\" where \"flightSerialNumber\" = '" + serial_number + "'");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    #endregion
    #region Luggage

    public async static Task<Result<bool, string>> AddLuggage(LuggageCassView t, string tId)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            
            RowSet luggage = s.Execute("insert into \"Luggage\" (\"ticketId\", number, weight, dimension, \"pricePerKG\")  values ('" + tId +"', '" + t.number +"'," + t.weight +",'" + t.dimension +"'," + t.pricePerKG +")");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    public async static Task<Result<List<LuggageCassView>, string>> GetLuggages(string ticketID)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            List<LuggageCassView> luggages = new List<LuggageCassView>();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            List<Row>? luggagesData = s.Execute("select * from \"Luggage\" where \"ticketId\" = '" + ticketID + "'").ToList();
            
            foreach (var luggageData in luggagesData)
            {
                if(luggageData != null)
                {
                    LuggageCassView luggage = new LuggageCassView
                    {
                        ticketId = luggageData["ticketId"] != null ? luggageData["ticketId"].ToString() : string.Empty,
                        number = luggageData["number"] != null ? luggageData["number"].ToString() : string.Empty,
                        weight = luggageData["weight"] != null ? float.Parse(luggageData["weight"].ToString()!) : 0,
                        dimension = luggageData["dimension"] != null ? luggageData["dimension"].ToString() : string.Empty,
                        pricePerKG = luggageData["pricePerKG"] != null ? float.Parse(luggageData["pricePerKG"].ToString()!) : 0
                    };
                    luggages.Add(luggage);
                }
            }
            
            return luggages;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<bool, string>> DeleteLuggages(string ticketID)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            s.Execute("delete from \"Luggage\" where \"ticketId\" = '" + ticketID + "'");
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    #endregion
}
