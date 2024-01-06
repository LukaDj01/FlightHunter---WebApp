using Cassandra;
using FHLibrary.QueryEntities;

namespace FHLibrary;
public static class CassandraDataProvider
{
    public async static Task<Result<bool, string>> AddFlight(ExpiredFlightView f)
    {
        try
        {
            ISession s = CassandraSessionManager.GetSession();

            if (s == null)
            {
                return "Nemoguće otvoriti sesiju. Cassandra";
            }
            RowSet hotelData = s.Execute("insert into \"Flight\" (\"flightID\", address, city, name, phone, state, zip)  values ('" + f.serial_number +"', 'Vozda Karadjordja 12', 'Nis', 'Grand', '123', 'Srbija', '18000')");
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
}
