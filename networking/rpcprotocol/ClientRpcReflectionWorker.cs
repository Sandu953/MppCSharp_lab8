using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using lab8.Domain;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using services;
using System.Buffers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace network
{
    public class ClientRpcReflectionWorker : IObserver
    {
        private IServices server;
        private TcpClient connection;

        //private BinaryReader input;
        //private BinaryWriter output;
        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public ClientRpcReflectionWorker(IServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                //NetworkStream networkStream = new NetworkStream(this.connection);
                //output = new BinaryWriter(networkStream);
                //input = new BinaryReader(networkStream);
                formatter = new BinaryFormatter();
                stream = connection.GetStream();
                connected = true;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void Run()
        {
            while (connected)
            {
                try
                {
                    //object request = SerializationUtils.Deserialize(input);
                    //object request = SerializationUtils.Deserialize(input); 
                    object request = formatter.Deserialize(stream);
                    Response response = HandleRequest((Request)request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Task.Delay(1000).Wait(); // Use Task.Delay instead of Thread.Sleep
            }
            try
            {
                //input.Close();
                //output.Close();
                stream.Close();
                connection.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error " + e);
            }
        }

        private Response OkResponse = new Response.Builder().Type(ResponseType.OK).Build();

        private Response HandleRequest(Request request)
        {
            Response response = null;
            string handlerName = "Handle" + request.Type.ToString();
            Console.WriteLine("HandlerName " + handlerName);
            try
            {
                MethodInfo method = this.GetType().GetMethod(handlerName, BindingFlags.NonPublic | BindingFlags.Instance);
                response = (Response)method.Invoke(this, new object[] { request });
                Console.WriteLine("Method " + handlerName + " invoked");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private Response HandleLOGIN(Request request)
        {
            Console.WriteLine("Login request ..." + request.Type);
            AgentieDTO udto = (AgentieDTO)request.Data;
            try
            {
                lock (server)
                {
                    if (!server.HandleLogin(udto.Username, udto.Password, this)) throw new Exception("Wrong username or password");
                }
                return OkResponse;
            }
            catch (Exception e)
            {
                connected = false;
                return new Response.Builder().Type(ResponseType.ERROR).Data(e.Message).Build();
            }
        }

        private void HandleLOGOUT(Request request)
        {
            Console.WriteLine("Logout request...");
            AgentieDTO udto = (AgentieDTO)request.Data;
            Agentie user = DTOUtils.GetFromDTO(udto);
            Console.WriteLine(user.Id);
            try
            {
                lock (server)
                {
                    server.Logout(user, this);
                }
                //server.Logout(user, this);
                connected = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void HandleGET_EXCURSII(Request request)
        {
            try
            {
                lock (server)
                {
                    Response resp = new Response.Builder().Type(ResponseType.EXCURSII).Data(server.GetAllExcursii()).Build();
                    SendResponse(resp);
                }
               // Response resp = new Response.Builder().Type(ResponseType.EXCURSII).Data(server.GetAllExcursii()).Build();
                //SendResponse(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void HandleGET_FREE_SEATS(Request request)
        {
            int id = (int)request.Data;
            try
            {
                lock (server)
                {
                    Response resp = new Response.Builder().Type(ResponseType.OK).Data(server.GetFreeSeats(id)).Build();
                    SendResponse(resp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void HandleGET_EXCURSII_ORE(Request request)
        {
            CautareDTO cautareDTO = (CautareDTO)request.Data;
            string ora1 = cautareDTO.Ora1;
            string ora2 = cautareDTO.Ora2;
            string obiectiv = cautareDTO.Obiectiv;
            try
            {
                lock (server)
                {
                    Response resp = new Response.Builder().Type(ResponseType.EXCURSII_ORE).Data(server.GetExcursiiBetweenHours(obiectiv, ora1, ora2)).Build();
                    SendResponse(resp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void HandleADD_REZ(Request request)
        {
            RezervareDTO rdto = (RezervareDTO)request.Data;
            Rezervare rezervare = DTOUtils.GetFromDTO(rdto);
            try
            {
                lock (server)
                {
                    server.AddRezervare(rdto.Id, rezervare.Excursie, rezervare.NumeClient, rezervare.NrTelefon, rezervare.NrLocuri);
                    Response resp = new Response.Builder().Type(ResponseType.ADDED_REZERVATION).Build();
                    SendResponse(resp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void HandleGET_ID(Request request)
        {
            AgentieDTO udto = (AgentieDTO)request.Data;
            try
            {
                lock (server)
                {
                    Response resp = new Response.Builder().Type(ResponseType.OK).Data(server.GetId(udto.Username, udto.Password)).Build();
                    SendResponse(resp);
                }
               // Response resp = new Response.Builder().Type(ResponseType.OK).Data(server.GetId(udto.Username, udto.Password)).Build();
               // SendResponse(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ReservationMade()
        {
            try
            {
                Response resp = new Response.Builder().Type(ResponseType.ADDED_REZERVATION).Build();
                SendResponse(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                formatter.Serialize(stream, response);
                stream.Flush();
            }
        }
    }
}
