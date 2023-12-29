using Neo4jClient;
using FHLibrary.DomainModel;

namespace FHLibrary;
public static class Neo4JDataProvider
{
    public async static Task<Result<bool, string>> AddPassanger()
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
                

            Dictionary<string, object> queryDict = new Dictionary<string, object>
            {
                { "ime", "proba" },
                { "prezime", "probic" },
                { "email", "proba@gmail.com" },
                { "sifra", "prosifra" }
            };

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Vlasnik {ime:'" + "proba"
                                                            + "',prezime:'" + "probic" + "', email:'" + "proba@gmail.com"
                                                            + "',sifra:'" + "prosifra"
                                                            + "'}) return n",
                                                            queryDict, Neo4jClient.Cypher.CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query);
            
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
