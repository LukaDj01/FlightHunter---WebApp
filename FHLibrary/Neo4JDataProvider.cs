using Neo4jClient;
using Neo4jClient.Cypher;

namespace FHLibrary;
public static class Neo4JDataProvider
{
    public static readonly GraphClient? c = Neo4JSessionManager.GetClient();

    #region AvioCompany
    public async static Task<Result<bool, string>> AddAvioCompany(AvioCompanyView ac)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:AvioCompany {email:'" + ac.email + "', password:'" + ac.password
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


    public async static Task<Result<List<AirportView>, string>> GetAirports()
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Airport) return n",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<AirportView>? airports = ((IRawGraphClient)c).ExecuteGetCypherResults<AirportView>(query).ToList();

            return airports!;
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
    public async static Task<Result<bool, string>> AddTicket(TicketsView a, string pemail, string flightSerialNumber)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>
            {
                { "purchaseDate", a.purchaseDate },
                { "price", a.price },
                { "seatNumber", a.seatNumber},
            };

            var queryMaxId = new CypherQuery("MATCH (t:Ticket) return max(t.id)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxId = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxId).ToList().FirstOrDefault();

            var id = "";
            if(maxId!=null)
            {
                int mId = Int32.Parse(maxId);
                id = (++mId).ToString();
            }
            else
                id="1";

            var query = new CypherQuery("CREATE (a:Ticket {id:'" + id
                                                            +"',purchaseDate:'" + a.purchaseDate
                                                            +"',price:'" + a.price
                                                            +"',seatNumber:'" + a.seatNumber
                                                            + "'}) return a",
                                new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query);

            var query2 = new CypherQuery("MATCH (p:Passenger {email:'" + pemail + "'}), (t:Ticket {id:'" + id + "'}), (ef:ExpiredFlight {serial_number:'" + flightSerialNumber + "'})"+
                                        "CREATE (p)-[:BUYS]->(t)-[:FOR]->(ef)",
                                        new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query2);
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


    public async static Task<Result<TicketsView, string>> GetTicket(string id)
    {
        try
        {
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


    public async static Task<Result<List<TicketsView>, string>> GetTicketsPass(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (p:Passenger {email:'" + email + "'})-[:BUYS]->(t:Ticket) return t",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<TicketsView>? tickets = ((IRawGraphClient)c).ExecuteGetCypherResults<TicketsView>(query).ToList();

            return tickets!;
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
    public async static Task<Result<bool, string>> AddExpiredFlight(ExpiredFlightView ef, string acEmail, string pib1, string pib2, string serialNumber)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var queryMaxId = new CypherQuery("MATCH (ef:ExpiredFlight) return max(ef.serial_number)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxId = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxId).ToList().FirstOrDefault();

            var serial_number = "";
            if(maxId!=null)
            {
                int mId = Int32.Parse(maxId);
                serial_number = (++mId).ToString();
            }
            else
                serial_number="1";

            var query = new CypherQuery("CREATE (ef:ExpiredFlight {serial_number:'" + serial_number
                                                            + "',capacity:'" + ef.capacity
                                                            + "',available_seats:'" + ef.available_seats
                                                            + "'}) return ef",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query);

            var query2 = new CypherQuery("MATCH (ac:AvioCompany {email: '" + acEmail + "'}), (ap1:Airport {pib: '" + pib1 + "'}), (ap2:Airport {pib: '" + pib2 + "'}), (p:Plane {serialNumber: '" + serialNumber + "'}), (ef:ExpiredFlight {serial_number:'" + ef.serial_number + "'})"+
                                        "CREATE (ac)-[:ORGANIZE]->(ef), (ap1)-[:TAKE_OFF]->(ef), (ap2)-[:LAND]->(ef), (p)-[:FLY]->(ef)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            ((IRawGraphClient)c).ExecuteCypher(query2);


            
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
    public async static Task<Result<ExpiredFlightView, string>> GetExpiredFlightTicket(string id)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (t:Ticket {id: '" + id + "'})-[:FOR]->(ef:ExpiredFlight) return ef",
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
    public async static Task<Result<List<ExpiredFlightView>, string>> GetExpiredFlightsAC(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (ac:AvioCompany {email: '"+email+"'})-[:ORGANIZE]->(ef:ExpiredFlight) return ef",
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
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("CREATE (n:Passenger {email:'" + p.email
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
     public async static Task<Result<PassengerView, string>> GetPassenger(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Passenger) and n.email ='" + email + "' return n limit 1",
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
    /*public async static Task<Result<bool, string>> DeletePassenger(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var exists = GetPassenger(email);
            if (exists == null)
            {
                return "Putnik sa datim e-mailom ne postoji";
            }

            var query = new CypherQuery("start n=node(*) where (n:Passenger) and n.email ='" + email + "' delete n",
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
    }*/

    public async static Task<Result<bool, string>> DeletePassenger(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
            
            var existsResult = await GetPassenger(email);
            if (existsResult.IsError)
            {
                return existsResult.Error;
            }

            var exists = existsResult.Data;
            if (exists == null)
            {
                return false;
            }

            var query = new CypherQuery("MATCH (n:Passenger {email: '" + email + "'}) DELETE n",
                                        new Dictionary<string, object>(), CypherResultMode.Projection);
            ((IRawGraphClient)c).ExecuteCypher(query);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return true;
    }

    
    #endregion
    #region Feedback
    

     public async static Task<Result<bool, string>> AddFeedbackPassAC(FeedbackView f, string passEmail, string acEmail)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var queryMaxId = new CypherQuery("MATCH ()-[f:FEEDBACK]->() return max(f.id)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxId = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxId).ToList().FirstOrDefault();

            var id = "";
            if(maxId!=null)
            {
                int mId = Int32.Parse(maxId);
                id = (++mId).ToString();
            }
            else
                id="1";

            var query = new CypherQuery("MATCH (p:Passenger {email: '" + passEmail + "'}), (ac:AvioCompany {email: '" + acEmail + "'})"
                                        + " CREATE (p)-[:FEEDBACK {id:'" + id
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
    

     public async static Task<Result<bool, string>> AddFeedbackPassAirport(FeedbackView f, string passEmail, string apPib)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var queryMaxId = new CypherQuery("MATCH ()-[f:FEEDBACK]->() return max(f.id)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxId = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxId).ToList().FirstOrDefault();

            var id = "";
            if(maxId!=null)
            {
                int mId = Int32.Parse(maxId);
                id = (++mId).ToString();
            }
            else
                id="1";

            var query = new CypherQuery("MATCH (p:Passenger {email: '" + passEmail + "'}), (ap:Airport {pib: '" + apPib + "'})"
                                        + " CREATE (p)-[:FEEDBACK {id:'" + id
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
     public async static Task<Result<List<FeedbackView>, string>> GetFeedbacksAC(string acEmail)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH ()-[f:FEEDBACK]->(:AvioCompany {email: '" + acEmail + "'}) RETURN f",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<FeedbackView> feeds = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).ToList();

            foreach (var feed in feeds)
            {
                query = new CypherQuery("MATCH (p:Passenger)-[f:FEEDBACK {id: '" + feed.id + "'}]->(ac:AvioCompany) RETURN p",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
                PassengerView? passenger = ((IRawGraphClient)c).ExecuteGetCypherResults<PassengerView>(query).FirstOrDefault();
                query = new CypherQuery("MATCH (p:Passenger)-[f:FEEDBACK {id: '" + feed.id + "'}]->(ac:AvioCompany) RETURN ac",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
                AvioCompanyView? ac = ((IRawGraphClient)c).ExecuteGetCypherResults<AvioCompanyView>(query).FirstOrDefault();
                feed.passenger=passenger;
                feed.avioCompany=ac;
            }
            
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
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH ()-[f:FEEDBACK]->(:Airport {pib: '" + airportPib + "'}) RETURN f",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<FeedbackView> feeds = ((IRawGraphClient)c).ExecuteGetCypherResults<FeedbackView>(query).ToList();

            foreach (var feed in feeds)
            {
                query = new CypherQuery("MATCH (p:Passenger)-[f:FEEDBACK {id: '" + feed.id + "'}]->(a:Airport) RETURN p",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
                PassengerView? passenger = ((IRawGraphClient)c).ExecuteGetCypherResults<PassengerView>(query).FirstOrDefault();
                query = new CypherQuery("MATCH (p:Passenger)-[f:FEEDBACK {id: '" + feed.id + "'}]->(a:Airport) RETURN a",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
                AirportView? a = ((IRawGraphClient)c).ExecuteGetCypherResults<AirportView>(query).FirstOrDefault();
                feed.passenger=passenger;
                feed.airport=a;
            }

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
    public async static Task<Result<bool, string>> AddLuggage(LuggageView l, string tId)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
            var queryMaxNumber = new CypherQuery("MATCH (t:Ticket {id: '" + tId + "'})-[:HAVE]->(l:Luggage) return max(l.number)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxNumber = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxNumber).ToList().FirstOrDefault();

            var number = "";
            if(maxNumber!=null)
            {
                int mId = Int32.Parse(maxNumber);
                number = (++mId).ToString();
            }
            else
                number="1";

            var query = new CypherQuery("MATCH (t:Ticket {id: '" + tId + "'})"
                                        + " CREATE (t)-[:HAVE]->(l:Luggage {  number:'" + number
                                                            + "',weight:'" + l.weight
                                                            + "',dimension:'" + l.dimension
                                                            + "',pricePerKG:'" + l.pricePerKG
                                                            + "'}) return l",
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

    public async static Task<Result<List<LuggageView>, string>> GetLuggagesTicket(string id)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("MATCH (t:Ticket {id: '" + id + "'})-[:HAVE]->(l:Luggage) return l",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<LuggageView>? luggages = ((IRawGraphClient)c).ExecuteGetCypherResults<LuggageView>(query).ToList();

            return luggages!;
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
    public async static Task<Result<bool, string>> AddPlane(PlaneView p, string acEmail)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var queryMaxId = new CypherQuery("MATCH (p:Plane) return max(p.serialNumber)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            String? maxId = ((IRawGraphClient)c).ExecuteGetCypherResults<String>(queryMaxId).ToList().FirstOrDefault();

            var serialNumber = "";
            if(maxId!=null)
            {
                int mId = Int32.Parse(maxId);
                serialNumber = (++mId).ToString();
            }
            else
                serialNumber="1";

            var query = new CypherQuery("MATCH (ac:AvioCompany {email: '"+acEmail+"'})"
                                        +" CREATE (ac)-[:OWNS]->(p:Plane {serialNumber:'" + serialNumber
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


    public async static Task<Result<PlaneView, string>> GetPlanes()
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }

            var query = new CypherQuery("start n=node(*) where (n:Plane) return n",
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


    public async static Task<Result<List<PlaneView>, string>> GetPlanesAvioComplany(string email)
    {
        try
        {
            if (c == null)
            {
                return "Nemoguće otvoriti sesiju. Neo4J";
            }
            
            var query = new CypherQuery("MATCH (ac:AvioCompany {email: '"+email+"'})-[:OWNS]->(p:Plane) return p",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);

            List<PlaneView>? planes = ((IRawGraphClient)c).ExecuteGetCypherResults<PlaneView>(query).ToList();

            return planes!;
        }
        catch (Exception e)
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

    
}
