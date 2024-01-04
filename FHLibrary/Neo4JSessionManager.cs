using Neo4jClient;

namespace FHLibrary;
public static class Neo4JSessionManager
{
    public static GraphClient? client;

    public static GraphClient? GetClient()
    {
        try
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "majl"); //neo4jneo4j
            client.Connect();
        }
        catch (Exception)
        {
            return null;
        }
            
        return client;
    }
}