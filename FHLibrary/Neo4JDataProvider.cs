﻿using Neo4jClient;
using FHLibrary.DomainModel;
using Neo4jClient.Cypher;

namespace FHLibrary;
public static class Neo4JDataProvider
{
    public async static Task<Result<bool, string>> AddAvioCompany(AvioCompanyView ac)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:AvioCompany {id:'" + ac.id
                                                            + "',email:'" + ac.email + "', password:'" + ac.password
                                                            + "',name:'" + ac.name + "', phone:'" + ac.phone
                                                            + "',state:'" + ac.state
                                                            + "'}) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
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

    
    public async static Task<Result<AvioCompanyView, string>> UpdateAvioCompany(AvioCompanyView ac)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:AvioCompany) and n.email ='" + ac.email + "' set n.password = '"+ ac.password
                                                                                                        + "' set n.name = '"+ ac.name
                                                                                                        + "' set n.phone = '"+ ac.phone
                                                                                                        + "' set n.state = '"+ ac.state
                                                                                                        +"' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            AvioCompanyView? avioCompany = ((IRawGraphClient)c).ExecuteGetCypherResults<AvioCompanyView>(query).FirstOrDefault();

            return avioCompany!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    
    public async static Task<Result<AvioCompanyView, string>> GetAvioCompany(string email)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:AvioCompany) and n.email ='" + email + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            AvioCompanyView? avioCompany = ((IRawGraphClient)c).ExecuteGetCypherResults<AvioCompanyView>(query).FirstOrDefault();

            return avioCompany!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    
    public async static Task<Result<bool, string>> DeleteAvioCompany(string email)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:AvioCompany) and n.email ='" + email + "' delete n",
                                                            new Dictionary<string, object>(), CypherResultMode.Projection);

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