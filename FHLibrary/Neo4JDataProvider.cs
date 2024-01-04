using Neo4jClient;
using FHLibrary.DomainModel;
using Neo4jClient.Cypher;

namespace FHLibrary;
public static class Neo4JDataProvider
{
    #region AvioCompany
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

    #endregion
    #region Airport
    public async static Task<Result<bool, string>> AddAirport(AirportView a)
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
                { "pib", a.pib },
                { "name", a.name },
                { "phone", a.phone },
                { "address", a.address },
                { "city", a.city },
                { "state", a.state },
                { "gateNumber", a.gateNumber }
            };

            var query = new Neo4jClient.Cypher.CypherQuery(
            $"CREATE (n:Airport {{ pib:'{a.pib}', name:'{a.name}', phone: '{a.phone}', address: '{a.address}', city: '{a.city}', state: '{a.state}', gateNumber: '{a.gateNumber}'}}) return n",
            queryDict, Neo4jClient.Cypher.CypherResultMode.Set);

            ((IRawGraphClient)c).ExecuteCypher(query);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return true;
    }

    public async static Task<Result<AirportView, string>> UpdateAirport(AirportView ac)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Airport) and n.phone ='" + ac.phone + "' set n.name = '" + ac.name
                                                                                                        + "' set n.city = '" + ac.city
                                                                                                        + "' set n.state = '" + ac.state
                                                                                                        + "' set n.address = '" + ac.address
                                                                                                        + "' set n.gateNumber = '" + ac.gateNumber
                                                                                                        + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            AirportView? airport = ((IRawGraphClient)c).ExecuteGetCypherResults<AirportView>(query).FirstOrDefault();

            return airport!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<AirportView, string>> GetAirport(string pib)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Airport) and n.pib ='" + pib + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            AirportView? airport = ((IRawGraphClient)c).ExecuteGetCypherResults<AirportView>(query).FirstOrDefault();

            return airport!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<bool, string>> DeleteAirport(string pib)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Airport) and n.pib ='" + pib + "' delete n",
                                                            new Dictionary<string, object>(), CypherResultMode.Projection);

            ((IRawGraphClient)c).ExecuteCypher(query);

        }
        catch (Exception e)
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
    public async static Task<Result<bool, string>> AddTicket(TicketsView a)
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
                { "purchaseDate", a.purchaseDate },
                { "price", a.price },
            };

            var query = new Neo4jClient.Cypher.CypherQuery(
            $"CREATE (n:Ticket {{ purchaseDate:'{a.purchaseDate}', price:'{a.price}'}}) return n",
            queryDict, Neo4jClient.Cypher.CypherResultMode.Set);

            ((IRawGraphClient)c).ExecuteCypher(query);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return true;
    }

    public async static Task<Result<TicketsView, string>> UpdateTicket(TicketsView ac)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Ticket) and n.purchaseDate ='" + ac.purchaseDate + "' set n.price = '" + ac.price
                                                                                                        + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            TicketsView? ticket = ((IRawGraphClient)c).ExecuteGetCypherResults<TicketsView>(query).FirstOrDefault();

            return ticket!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<TicketsView, string>> GetTickets(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Ticket) and n.id ='" + id + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            TicketsView? ticket = ((IRawGraphClient)c).ExecuteGetCypherResults<TicketsView>(query).FirstOrDefault();

            return ticket!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<bool, string>> DeleteTickets(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Ticket) and n.id ='" + id + "' delete n",
                                                            new Dictionary<string, object>(), CypherResultMode.Projection);

            ((IRawGraphClient)c).ExecuteCypher(query);

        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }

        return true;
    }

    #endregion
    #region ExpiredFlight
    public async static Task<Result<bool, string>> AddExpiredFlight(ExpiredFlightView ef)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:ExpiredFlight {serial_number:'" + ef.serial_number
                                                            + "',capacity:'" + ef.capacity
                                                            + "',available_seats:'" + ef.available_seats
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
    public async static Task<Result<ExpiredFlightView, string>> GetExpiredFlight(string serial_number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:ExpiredFlight) and n.serial_number ='" + serial_number + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            ExpiredFlightView? expFlight = ((IRawGraphClient)c).ExecuteGetCypherResults<ExpiredFlightView>(query).FirstOrDefault();

            return expFlight!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<List<ExpiredFlightView>, string>> GetExpiredFlights()
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:ExpiredFlight) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<ExpiredFlightView> expFlights = ((IRawGraphClient)c).ExecuteGetCypherResults<ExpiredFlightView>(query).ToList();

            return expFlights!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }

    
    public async static Task<Result<bool, string>> DeleteExpiredFlight(string serial_number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:ExpiredFlight) and n.serial_number ='" + serial_number + "' delete n",
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

    #endregion
    #region Passenger
    public async static Task<Result<bool, string>> AddPassenger(PassengerView p)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:Passenger {id:'" + p.id
                                                            + "',email:'" + p.email
                                                            + "',password:'" + p.password
                                                            + "',passport:'" + p.passport
                                                            + "',phone:'" + p.phone
                                                            + "',birth_date:'" + p.birth_date
                                                            + "',nationality:'" + p.nationality
                                                            + "',first_name:'" + p.first_name
                                                            + "',last_name:'" + p.last_name
                                                            + "',addr_street:'" + p.addr_street
                                                            + "',addr_stNo:'" + p.addr_stNo
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
     public async static Task<Result<PassengerView, string>> GetPassenger(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Passenger) and n.id ='" + id + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            PassengerView? pass = ((IRawGraphClient)c).ExecuteGetCypherResults<PassengerView>(query).FirstOrDefault();

            return pass!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
     public async static Task<Result<List<PassengerView>, string>> GetPassenger()
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Passenger) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<PassengerView> pass = ((IRawGraphClient)c).ExecuteGetCypherResults<PassengerView>(query).ToList();

            return pass!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
    public async static Task<Result<bool, string>> DeletePassenger(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Passenger) and n.id ='" + id + "' delete n",
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
    #endregion
    #region Feedback
    

     public async static Task<Result<bool, string>> AddFeedback(FeedbackView f)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:Feedback {id:'" + f.id
                                                            + "',date:'" + f.date
                                                            + "',comment:'" + f.comment
                                                            + "',rate:'" + f.rate
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
     public async static Task<Result<FeedbackView, string>> GetFeedback(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Feedback) and n.id ='" + id + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            FeedbackView? feed = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).FirstOrDefault();

            return feed!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
     public async static Task<Result<List<FeedbackView>, string>> GetFeedback()
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Feedback) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<FeedbackView> feed = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).ToList();

            return feed!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
     public async static Task<Result<bool, string>> DeleteFeedback(string id)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Feedback) and n.id ='" + id + "' delete n",
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
    #endregion
}
