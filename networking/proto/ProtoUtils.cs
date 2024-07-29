using Google.Protobuf.WellKnownTypes;
using Org.Example;
//using Agency = model.Agency;
//using Booking = model.Booking;
//using Client = model.Client;







public class ProtoUtils
{
    public static Response createOkResponse()
    {
        //create a response for a successful request
        Org.Example.Response response = new Org.Example.Response { Type = Org.Example.Response.Types.ResponseType.Ok};
        return response;


        //Response response = new Response{Type = Response.Type.Ok};
        //return response;
    }
    
    public static Response createErrorResponse(string err)
    {
        Response response = new Response {Type = Response.Types.ResponseType.Error, Error = err};
        return response;
    }

    public static Agentie fromAgentie(lab8.Domain.Agentie agentie)
    {
        Agentie agentieDTO = new Agentie
        {
            Id = agentie.Id,
            Username = agentie.Username,
            Pass = ""
        };

        return agentieDTO;
    }

    public static lab8.Domain.Agentie getAgentie(Agentie agentie)
    {
        lab8.Domain.Agentie agentieDTO = new lab8.Domain.Agentie
        (
            agentie.Id,
            agentie.Username
            
        );
       

        return agentieDTO;
    }

    //public static ProjectResponse createGetFlightsResponse(model.Flight[] flights)
    //{
    //    ProjectResponse response = new ProjectResponse {Type = ProjectResponse.Types.Type.Flights};
    //    foreach (var flight in flights)
    //    {
    //       response.Flights.Add(fromFlight(flight));
    //    }
    //    return response;
    //}

    //public static ProjectResponse createGetFilteredFlightsResponse(model.Flight[] flights)
    //{
    //    ProjectResponse response = new ProjectResponse {Type = ProjectResponse.Types.Type.FilteredFlights};
    //    foreach (var flight in flights)
    //    {
    //        response.Flights.Add(fromFlight(flight));
    //    }
    //    return response;
    //}

    public static lab8.Domain.Agentie getAgentieFromRequest(Request request)
    {
        lab8.Domain.Agentie agency = new lab8.Domain.Agentie(request.Agentie.Id, request.Agentie.Username);
        return agency;
    }

    //public static ProjectResponse createNumberOfSeatsResponse(int numberOfSeats)
    //{
    //    ProjectResponse response = new ProjectResponse {Type = ProjectResponse.Types.Type.NumberOfSeats};
    //    response.Number = numberOfSeats;
    //    return response;
    //}

    //public static Booking getBooking(ProjectRequest request)
    //{
    //    List<String> passengers = new List<String>();

    //    var size = request.Booking.Passengers.Count;

    //    for (var i = 0; i < size; ++i)
    //    {
    //        passengers.Add(request.Booking.Passengers[i]);
    //    }

    //    Booking booking = new Booking(
    //        getFlight(request.Booking.Flight),
    //        getClient(request.Booking.Client),
    //        passengers
    //    );

    //    return booking;
    //}

    //private static Client getClient(Org.Example.Client bookingClient)
    //{
    //    Client client = new Client(bookingClient.Name, bookingClient.Address);
    //    client.Id = bookingClient.Id;

    //    return client;
    //}

    //public static ProjectResponse createUpdateResponse()
    //{
    //    ProjectResponse response = new ProjectResponse {Type = ProjectResponse.Types.Type.Booking};
    //    return response;
    //}
}