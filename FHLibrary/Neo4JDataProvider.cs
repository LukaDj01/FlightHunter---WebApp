﻿using Neo4jClient;
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

            var query = new CypherQuery("start n=node(*) where (n:Airport) and n.pib ='" + ac.pib + "' set n.name = '" + ac.name
                                                                                                        + "' set n.city = '" + ac.city
                                                                                                        + "' set n.state = '" + ac.state
                                                                                                        + "' set n.address = '" + ac.address
                                                                                                        + "' set n.phone = '" + ac.phone
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
    

     public async static Task<Result<bool, string>> AddFeedbackPassAC(FeedbackView f, string passId, string acId)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (p:Passenger {id: '" + passId + "'}), (ac:AvioCompany {id: '" + acId + "'})"
                                        + " CREATE (p)-[:FEEDBACK {id:'" + f.id
                                                            + "',date:'" + f.date
                                                            + "',comment:'" + f.comment
                                                            + "',rate:'" + f.rate
                                                            + "'}]->(ac)",
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
    

     public async static Task<Result<bool, string>> AddFeedbackPassAirport(FeedbackView f, string passId, string apPib)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (p:Passenger {id: '" + passId + "'}), (ap:Airport {pib: '" + apPib + "'})"
                                        + " CREATE (p)-[:FEEDBACK {id:'" + f.id
                                                            + "',date:'" + f.date
                                                            + "',comment:'" + f.comment
                                                            + "',rate:'" + f.rate
                                                            + "'}]->(ap)",
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
     public async static Task<Result<List<FeedbackView>, string>> GetFeedbacksAC(string acId)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH ()-[f:FEEDBACK]->(:AvioCompany {id: '" + acId + "'}) RETURN f",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<FeedbackView> feeds = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).ToList();

            return feeds!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }
     public async static Task<Result<List<FeedbackView>, string>> GetFeedbacksAirport(string airportPib)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH ()-[f:FEEDBACK]->(:Airport {pib: '" + airportPib + "'}) RETURN f",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<FeedbackView> feeds = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).ToList();

            return feeds!;
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

            var query = new CypherQuery("MATCH ()-[f:FEEDBACK]->() where f.id ='" + id + "' delete f",
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


    #region Luggage
    public async static Task<Result<bool, string>> AddLuggage(LuggageView l)
    {
        try
        {

            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:Luggage {number:'" + l.number
                                                            + "',weight:'" + l.weight
                                                            + "',dimension:'" + l.dimension
                                                            + "',pricePerKG:'" + l.pricePerKG
                                                            + "'}) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query);

        }
        catch (Exception e)
        {
            return e.Message;
        }

        return true;
    }

    public async static Task<Result<LuggageView, string>> UpdateLuggage(LuggageView l)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Luggage) and n.number ='" + l.number 
                                                                                                        + "' set n.weight = '"+ l.weight
                                                                                                        + "' set n.dimension = '"+ l.dimension
                                                                                                        + "' set n.pricePerKG = '"+ l.pricePerKG
                                                                                                        +"' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            LuggageView? luggage = ((IRawGraphClient)c).ExecuteGetCypherResults<LuggageView>(query).FirstOrDefault();

            return luggage!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<LuggageView, string>> GetLuggage(string number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Luggage) and n.number ='" + number + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            LuggageView? luggage = ((IRawGraphClient)c).ExecuteGetCypherResults<LuggageView>(query).FirstOrDefault();

            return luggage!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<bool, string>> DeleteLuggage(string number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Luggage) and n.number ='" + number + "' delete n",
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

    #region Plane
    public async static Task<Result<bool, string>> AddPlane(PlaneView p, string acId)
    {
        try
        {

            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (ac:AvioCompany {id: '"+acId+"'})"
                                        +" CREATE (ac)-[:OWNS]->(p:Plane {serialNumber:'" + p.serialNumber
                                                                        + "',fuel:'" + p.fuel
                                                                        + "',type:'" + p.type
                                                                        + "'}) return p",
                                                                        new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query);

        }
        catch (Exception e)
        {
            return e.Message;
        }

        return true;
    }

    public async static Task<Result<PlaneView, string>> UpdatePlane(PlaneView p)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Plane) and n.serialNumber ='" + p.serialNumber 
                                                                                                        + "' set n.fuel = '"+ p.fuel
                                                                                                        + "' set n.type = '"+ p.type
                                                                                                        +"' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            PlaneView? plane = ((IRawGraphClient)c).ExecuteGetCypherResults<PlaneView>(query).FirstOrDefault();

            return plane!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<PlaneView, string>> GetPlane(string serial_number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Plane) and n.serialNumber ='" + serial_number + "' return n limit 1",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            PlaneView? plane = ((IRawGraphClient)c).ExecuteGetCypherResults<PlaneView>(query).FirstOrDefault();

            return plane!;
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
        }
    }

         public async static Task<Result<List<PlaneView>, string>> GetPlanes()
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Plane) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<PlaneView> plane = ((IRawGraphClient)c).ExecuteGetCypherResults<PlaneView>(query).ToList();

            return plane!;
        }
        catch (Exception e )
        {
            return e.Message;
        }
        finally
        {
        }
    }


    public async static Task<Result<bool, string>> DeleteACPlaneRel(string serial_number)
    {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH ()-[o:OWNS]->(p:Plane {serialNumber: '"+serial_number+"'}) delete o",
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

    /*#region FeedbackRelationship

     public async static Task<Result<bool, string>> AddRelFeedAvioComp(string passId, string acId)
     {
        try
        {
            GraphClient? c = Neo4JSessionManager.GetClient();

            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
            //MATCH (charlie:Person {name: 'Charlie Sheen'}), (oliver:Person {name: 'Oliver Stone'})
            //CREATE (charlie)-[:ACTED_IN {role: 'Bud Fox'}]->(wallStreet:Movie {title: 'Wall Street'})<-[:DIRECTED]-(oliver)
            
            var query = new CypherQuery("MATCH (p:Passenger {n.id ='" + passId + "'}), (ac:AvioCompany {id: '" + acId + "'})"
                                        + " CREATE (p)-[:RATING {id:'" + f.id
                                                            + "',date:'" + f.date
                                                            + "',comment:'" + f.comment
                                                            + "',rate:'" + f.rate
                                                            + "'}]->(ac)",
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


    #endregion*/
}
