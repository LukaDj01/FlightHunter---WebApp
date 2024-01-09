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
    public async static Task<Result<FlightView, string>> GetFlight(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();
            FlightView flight = new FlightView();

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
                flight.avioCompanyId = flightData["avioCompanyId"] != null ? flightData["avioCompanyId"].ToString() : string.Empty;
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
    public async static Task<Result<bool, string>> UpdateFlightBuyTicket(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            FlightView flight = new FlightView();
            Row? flightData = s.Execute("select * from \"Flight\" where serial_number = '" + serial_number + "'").FirstOrDefault();

            if(flightData != null)
            {
                flight.capacity = flightData["capacity"] != null ? int.Parse(flightData["capacity"].ToString()!) : 0;
                flight.available_seats = flightData["available_seats"] != null ? int.Parse(flightData["available_seats"].ToString()!) : 0;
            }
            else return false;

            if(flight.available_seats > 0)
            {
                flight.available_seats--;
                s.Execute("update \"Flight\" set available_seats='" + flight.available_seats + "' where serial_number = '" + serial_number + "'");
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
    public async static Task<Result<bool, string>> AddFlight(FlightView f)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            f.serial_number = generateID();

            RowSet flightData = s.Execute("insert into \"Flight\" (serial_number, capacity, available_seats, \"avioCompanyId\", \"landAirportPib\", \"takeOffAirportPib\", \"planeSerialNumber\", \"dateTimeLand\", \"dateTimeTakeOff\", \"gateLand\", \"gateTakeOff\")  values ('" + f.serial_number +"', '" + f.capacity +"','" + f.available_seats +"','" + f.avioCompanyId +"','" + f.landAirportPib +"','" + f.takeOffAirportPib +"','" + f.planeSerialNumber +"','" + f.dateTimeLand +"','" + f.dateTimeTakeOff +"','" + f.gateLand +"','" + f.gateTakeOff +"')");
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
    public async static Task<Result<bool, string>> DeleteFlight(string serial_number)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }

            RowSet flightData = s.Execute("delete from \"Flight\" where serial_number = '" + serial_number + "'");
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

    public async static Task<Result<bool, string>> AddTicket(TicketsCassView t)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            
            t.id = generateID();

            RowSet flightData = s.Execute("insert into \"Ticket\" (\"passengerEmail\", \"flightSerialNumber\", purchaseDate, id, price, seatNumber)  values ('" + t.passengerEmail +"', '" + t.flightSerialNumber +"','" + t.purchaseDate +"','" + t.id +"','" + t.price +"','" + t.seatNumber +"')");
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

    public async static Task<Result<bool, string>> AddLuggage(LuggageCassView t)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            Row? luggageData = s.Execute("select * from \"Luggage\" where ticketId = '" + t.ticketId + "' order by number desc").FirstOrDefault();

            int number = 1;
            if(luggageData != null)
            {
                int num = luggageData["number"] != null ? int.Parse(luggageData["number"].ToString()!) : 0;
                if(num>=number) number = num+1;
            }
            

            RowSet luggage = s.Execute("insert into \"Luggage\" (\"ticketId\", number, weight, dimension, pricePerKG)  values ('" + t.ticketId +"', '" + number.ToString() +"','" + t.weight +"','" + t.dimension +"','" + t.pricePerKG +"')");
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
