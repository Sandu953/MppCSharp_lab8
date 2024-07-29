using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using lab8.Domain;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using services;

namespace network
{
    public class ServicesRpcProxy : IServices
    {
        private string host;
        private int port;

        private IObserver client;

        //private BinaryReader input;
        //private BinaryWriter output;
        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private BlockingCollection<Response> qresponses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public ServicesRpcProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            qresponses = new BlockingCollection<Response>();
        }

        private void CloseConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendRequest(Request request)
        {
            try
            {
                //byte[] requestData = SerializationUtils.Serialize(request);
                //output.Write(requestData.Length);
                //output.Write(requestData);
                //output.Flush();
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (IOException e)
            {
                throw new Exception("Error sending object " + e);
            }
        }

        private Response ReadResponse()
        {
            Response response = null;
            try
            {
                //int responseSize = input.ReadInt32();
                //byte[] responseData = new byte[responseSize];
                //input.Read(responseData, 0, responseSize);
                //response = SerializationUtils.Deserialize<Response>(responseData);
                _waitHandle.WaitOne();
                lock (qresponses)
                {
                    // monitor client
                    response = qresponses.Take();
                    
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                //output = new BinaryWriter(connection.GetStream());
                //input = new BinaryReader(connection.GetStream());
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void StartReader()
        {
            Thread tw = new Thread(new ThreadStart(ReaderThread));
            tw.Start();
        }

        private void HandleUpdate(Response response)
        {
            if (response.Type == ResponseType.UPDATE_SHEARCH || response.Type == ResponseType.ADDED_REZERVATION)
            {
                try
                {
                    client.ReservationMade();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }



        private bool IsUpdate(Response response)
        {
            return response.Type == ResponseType.UPDATE_SHEARCH || response.Type == ResponseType.ADDED_REZERVATION;
        }

        public bool HandleLogin(string username, string password, IObserver client)
        {
            InitializeConnection();
            AgentieDTO agentieDTO = new AgentieDTO(1,username, password);
            Request req = new Request.Builder().Type(RequestType.LOGIN).Data(agentieDTO).Build();
            SendRequest(req);
            Response response = ReadResponse();

            if (response.Type == ResponseType.OK)
            {
                this.client = client;
                return true;
            }
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                CloseConnection();
                return false;
            }
            return false;
        }

        public void Logout(Agentie user, IObserver client)
        {
            AgentieDTO udto = DTOUtils.GetDTO(user);
            Request req = new Request.Builder().Type(RequestType.LOGOUT).Data(udto).Build();
            SendRequest(req);
            Response response = ReadResponse();
            CloseConnection();
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                throw new Exception(err);
            }
        }

        // Implement other methods here...

        public  IEnumerable<Excursie> GetAllExcursii()
        {
            Request req = new Request.Builder().Type(RequestType.GET_EXCURSII).Build();
            SendRequest(req);
            Response response = ReadResponse();
            return (IEnumerable<Excursie>)response.Data;
        }

        public  IEnumerable<Excursie> GetExcursiiBetweenHours(string obiectiv, string ora1, string ora2)
        {
            CautareDTO cautareDTO = new CautareDTO(obiectiv, ora1, ora2);
            Request req = new Request.Builder().Type(RequestType.GET_EXCURSII_ORE).Data(cautareDTO).Build();
            SendRequest(req);
            Response response =  ReadResponse();
            return (IEnumerable<Excursie>)response.Data;
        }

        public int GetFreeSeats(int id)
        {
            Request req = new Request.Builder().Type(RequestType.GET_FREE_SEATS).Data(id).Build();
            SendRequest(req);
            Response response = ReadResponse();
            return (int)response.Data;
        }

        public void AddRezervare(long id, long ex, string nume, string telefon, int bilet)
        {
            Rezervare rez = new Rezervare(id,ex, nume, telefon, bilet);
            RezervareDTO rdto = DTOUtils.GetDTO(rez);
            rdto.Id = id;
            Console.WriteLine("ADD_REZ request");
            Request request = new Request.Builder().Type(RequestType.ADD_REZ).Data(rdto).Build();
            SendRequest(request);
            Response response = ReadResponse();
            Console.WriteLine(response.Type);
            if (response.Type == ResponseType.ERROR)
            {
                string err = response.Data.ToString();
                Console.WriteLine("Aici se inchide" + err);
                CloseConnection();
                throw new Exception(err);
            }
        }

        public  long GetId(string username, string password)
        {
            AgentieDTO agentieDTO = new AgentieDTO(1,username, password);
            Request request = new Request.Builder().Type(RequestType.GET_ID).Data(agentieDTO).Build();
            SendRequest(request);
            Response response = ReadResponse();
            return (long)response.Data;
        }


        private void ReaderThread()
        {
            while (!finished)
            {
                try
                {
                    //int responseSize = input.ReadInt32();
                    //byte[] responseData = new byte[responseSize];
                    //input.Read(responseData, 0, responseSize);
                    //Response response = (Response)formatter.Deserialize(stream);
                    object response = formatter.Deserialize(stream);
                    //Response response = SerializationUtils.Deserialize<Response>(responseData);
                    Console.WriteLine("response received " + response);
                    if (IsUpdate((Response)response))
                    {
                        HandleUpdate((Response)response);
                    }
                    else
                    {
                        try
                        {
                            qresponses.Add((Response)response);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }
    }
}