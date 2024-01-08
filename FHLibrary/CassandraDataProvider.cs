using Cassandra;

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

            /*if(flightData != null)
            {
                flight.serial_number = flightData["serial_number"] != null ? flightData["serial_number"].ToString() : string.Empty;
                flight.capacity = flightData["capacity"] != null ? Int32.Parse(flightData["capacity"]) : 0;
                flight.address = flightData["address"] != null ? flightData["address"].ToString() : string.Empty;
                flight.city = flightData["city"] != null ? flightData["city"].ToString() : string.Empty;
                flight.phone = flightData["phone"] != null ? flightData["phone"].ToString() : string.Empty;
                flight.state = flightData["state"] != null ? flightData["state"].ToString() : string.Empty;
                flight.zip = flightData["zip"] != null ? flightData["zip"].ToString() : string.Empty;
            }*/
            
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

            RowSet flightData = s.Execute("insert into \"Flight\" (serial_number, capacity, available_seats, \"avioCompanyId\", \"landAirportPib\", \"takeOffAirportPib\", \"planeSerialNumber\", dateTimeLand, dateTimeTakeOff, gateLand, gateTakeOff)  values ('" + f.serial_number +"', '" + f.capacity +"','" + f.available_seats +"','" + f.avioCompanyId +"','" + f.landAirportPib +"','" + f.takeOffAirportPib +"','" + f.planeSerialNumber +"','" + f.dateTimeLand +"','" + f.dateTimeTakeOff +"','" + f.gateLand +"','" + f.gateTakeOff +"')");
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
}
