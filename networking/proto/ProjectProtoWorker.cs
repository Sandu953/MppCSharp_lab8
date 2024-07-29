using System.Net.Sockets;
using Google.Protobuf;
using Org.Example;
using services;
using lab8.Domain;
using Org.Example;
using Google.Protobuf.WellKnownTypes;




public class ProjectProtoWorker:IObserver
{
    private IServices server;
    private TcpClient connection;

    private NetworkStream stream;
    private volatile bool connected;
    
    public ProjectProtoWorker(IServices server, TcpClient connection)
    {
        this.server = server;
        this.connection = connection;
        try
        {
            stream=connection.GetStream();
            connected=true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
    
    public virtual void run()
    {
        while(connected)
        {
            try
            {
					
                Request request = Request.Parser.ParseDelimitedFrom(stream);
                Response response = handleRequest(request);
                if (response != null)
                {
                    sendResponse(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
				
            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        try
        {
            stream.Close();
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error "+e);
        }
    }

    private void sendResponse(Response response)
    {
        Console.WriteLine("Sending response ...");
        lock (stream)
        {
            Console.WriteLine(response);
            response.WriteDelimitedTo(stream);
            stream.Flush();
            Console.WriteLine("Response sent ...");
        }
    }

    private Response handleRequest(Request request)
    {
        Response response = null;
        if (request.Type == Request.Types.RequestType.Login)
        {
            Console.WriteLine("Login request ...");
            //lab8.Domain.Agentie agency = ProtoUtils.getAgencie(request);
            try
            {
                lock (server)
                {
                    if (!server.HandleLogin(request.Agentie.Username, request.Agentie.Pass, this)) throw new Exception("Nume sau parola gresite!");
                }
                Console.WriteLine("Login OK");
                return ProtoUtils.createOkResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine("Login error " + e);
                connected = false;
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        
        if (request.Type == Request.Types.RequestType.Logout)
        {
            Console.WriteLine("Logout request...");
            lab8.Domain.Agentie agency = ProtoUtils.getAgentieFromRequest(request);
            try
            {
                lock (server)
                {
                    server.Logout(agency, this);
                }
                connected = false;
                return ProtoUtils.createOkResponse();
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == Request.Types.RequestType.GetExcursii)
        {
            Console.WriteLine("All excursii request");
            try
            {
                lock (server)
                {
                    IEnumerable<lab8.Domain.Excursie> excursii = server.GetAllExcursii();
                    Org.Example.Response res = new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.Excursii };
                    foreach (var excursie in excursii)
                    {
                        res.Excursii.Add(new Org.Example.Excursie{Id = excursie.Id, ObiectivTuristic = excursie.ObiectivTuristic, NumeTransport = excursie.NumeTransport, OraPlecare = excursie.OraPlecare, Pret = excursie.Pret, NrLocuri = excursie.NrLocuri, LocuriLibere = excursie.LocuriLibere });
                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == Request.Types.RequestType.GetExcursiiOre)
        {
            //Console.WriteLine("Filtered flights requested");
            //model.Flight flight = ProtoUtils.getFlight(request.Flight);
            //try
            //{
            //    model.Flight[] flights;
            //    lock (server)
            //    {
            //        flights = server.getFlightsForDestinationAndDate(flight, this);
            //    }

            //    return ProtoUtils.createGetFilteredFlightsResponse(flights);
            //}
            //catch (ProjectException e)
            //{
            //    return ProtoUtils.createErrorResponse(e.Message);
            //}
            Console.WriteLine("Get excursii ora");
            try
            {
                lock (server)
                {
                    string[] parts = request.Excursie.OraPlecare.Split(new string[] { "___" }, StringSplitOptions.None);

                    Console.WriteLine(parts[0]);
                    Console.WriteLine(parts[1]);
                    string ora1 = parts[0]; 
                    string ora2 = parts[1];
                    string[] formats = { "HH:mm" };
                    DateTime timestamp;

                    // Try parsing the string into a DateTime object
                    if (DateTime.TryParseExact(ora1, formats, null, System.Globalization.DateTimeStyles.None, out timestamp) && DateTime.TryParseExact(ora2, formats, null, System.Globalization.DateTimeStyles.None, out timestamp))
                    {
                        // Conversion successful, timestamp contains the time in DateTime format
                        TimeSpan ts = TimeSpan.Parse(ora1);
                        TimeSpan ts2 = TimeSpan.Parse(ora2);
                        long timeOra1 = (long)ts.TotalMilliseconds;
                        long timeOra2 = (long)ts2.TotalMilliseconds;
                        Console.WriteLine(timeOra1);
                        Console.WriteLine(timeOra2);
                        IEnumerable<lab8.Domain.Excursie> excursii = server.GetExcursiiBetweenHours(request.Excursie.ObiectivTuristic, timeOra1.ToString(), timeOra2.ToString());
                        Org.Example.Response res = new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.ExcursiiOre };
                        foreach (var excursie in excursii)
                        {
                            Console.WriteLine(excursie.ObiectivTuristic);
                            res.Excursii.Add(new Org.Example.Excursie { Id = excursie.Id, ObiectivTuristic = excursie.ObiectivTuristic, NumeTransport = excursie.NumeTransport, OraPlecare = excursie.OraPlecare, Pret = excursie.Pret, NrLocuri = excursie.NrLocuri, LocuriLibere = excursie.LocuriLibere });
                        }
                        return res;
                    }
                    else
                    {
                        IEnumerable<lab8.Domain.Excursie> excursii = server.GetExcursiiBetweenHours(request.Excursie.ObiectivTuristic, ora1, ora2);
                        Org.Example.Response res = new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.ExcursiiOre };
                        foreach (var excursie in excursii)
                        {
                            Console.WriteLine(excursie.ObiectivTuristic);
                            res.Excursii.Add(new Org.Example.Excursie { Id = excursie.Id, ObiectivTuristic = excursie.ObiectivTuristic, NumeTransport = excursie.NumeTransport, OraPlecare = excursie.OraPlecare, Pret = excursie.Pret, NrLocuri = excursie.NrLocuri, LocuriLibere = excursie.LocuriLibere });
                        }
                        return res;
                    }
                   
                    
                    
                    //Org.Example.Response res = new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.ExcursiiOre };
                    //foreach (var excursie in excursii)
                    //{
                    //    Console.WriteLine(excursie.ObiectivTuristic);
                    //    res.Excursii.Add(new Org.Example.Excursie { Id = excursie.Id, ObiectivTuristic = excursie.ObiectivTuristic, NumeTransport = excursie.NumeTransport, OraPlecare = excursie.OraPlecare, Pret = excursie.Pret, NrLocuri = excursie.NrLocuri, LocuriLibere = excursie.LocuriLibere });
                    //}
                    //return res;
                }
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == Request.Types.RequestType.GetFreeSeats)
        {
           
            Console.WriteLine("Get free seats");
            try
            {
                lock (server)
                {
                    int nr = server.GetFreeSeats((int)request.Excursie.Id);
                    return new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.Ok, Excursie = new Org.Example.Excursie { LocuriLibere = nr} };
                }
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        if (request.Type == Request.Types.RequestType.GetId)
        {
            //Console.WriteLine("Booking request...");
            //model.Booking booking = ProtoUtils.getBooking(request);

            //try
            //{
            //    lock (server)
            //    {
            //        server.bookFlight(booking, this);
            //    }
            //    notifyAgencies();
            //    return ProtoUtils.createOkResponse();
            //}
            //catch (ProjectException e)
            //{
            //    return ProtoUtils.createErrorResponse(e.Message);
            //}
            Console.WriteLine("Get id request");
            try
            {
                lock (server)
                {
                    long id = server.GetId(request.Agentie.Username, request.Agentie.Pass);
                    return new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.Ok, Agentie = new Org.Example.Agentie { Id = id } };
                }
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }
        if (request.Type == Request.Types.RequestType.UpdateSearch)
        {
            //Console.WriteLine("Update search request...");
            //notifyAgencies();
            //return ProtoUtils.createOkResponse();
            throw new NotImplementedException();
        }
        if (request.Type == Request.Types.RequestType.AddRez)
        {
            //Console.WriteLine("Add rez request...");
            //ReservationMade();
            //return ProtoUtils.createOkResponse();
            Console.WriteLine("Add rez request");
            try
            {
                lock (server)
                {
                    server.AddRezervare(request.Rezervare.Id, request.Rezervare.Excursie, request.Rezervare.NumeClient, request.Rezervare.NrTelefon, request.Rezervare.NrLocuri);
                    //return new Org.Example.Response { Type = Response.Types.ResponseType.AddedRezervation };
                }
                return ProtoUtils.createOkResponse();
                
            }
            catch (Exception e)
            {
                return ProtoUtils.createErrorResponse(e.Message);
            }
        }

        return response;
    }

    public void notifyAgencies()
    {
        //sendResponse(ProtoUtils.createUpdateResponse());
    }

    public void ReservationMade()
    {
        Console.WriteLine("Reservation made");
        try
        {
            lock (server)
            {

                sendResponse(ProtoUtils.createOkResponse());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }
}