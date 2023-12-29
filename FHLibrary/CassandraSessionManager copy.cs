using Cassandra;

namespace FHLibrary;
public static class CassandraSessionManager
{
    public static ISession? session;

    public static ISession GetSession()
    {
        if(session == null)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("FlightHunter");
        }

        return session;
    }
}