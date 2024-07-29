//using System.Net;
//using System.Net.Sockets;

//namespace network
//{

//    public abstract class AbstractServerProto
//    {
//        private TcpListener server;
//        private String host;
//        private int port;
//        public AbstractServerProto(String host, int port)
//        {
//            this.host = host;
//            this.port = port;
//        }
//        public void Start()
//        {
//            IPAddress adr = IPAddress.Parse(host);
//            IPEndPoint ep=new IPEndPoint(adr,port);
//            server=new TcpListener(ep);
//            server.Start();
//            while (true)
//            {
//                Console.WriteLine("Waiting for clients ...");
//                TcpClient client = server.AcceptTcpClient();
//                Console.WriteLine("Client connected ...");
//                processRequest(client);
//            }
//        }

//        public abstract void processRequest(TcpClient client);

//    }


//        public abstract class ConcurrentServerProto:AbstractServerProto
//        {

//            public ConcurrentServerProto(string host, int port) : base(host, port)
//            {}

//            public override void processRequest(TcpClient client)
//            {

//                Thread t = createWorker(client);
//                t.Start();

//            }

//            protected abstract  Thread createWorker(TcpClient client);

//        }

//}
using Google.Protobuf;
using lab8.Domain;
using network;
using Org.Example;
using services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace networking
{


    public class RPCConcurrentServerProto : AbsConcurrentServer
    {
        private IServices server;
        private ProjectProtoWorker worker;
        public RPCConcurrentServerProto(string host, int port, IServices server) : base( port)
        {
            this.server = server;
            Console.WriteLine("RPCConcurrentServer...");
        }
        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ProjectProtoWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }

    }

    public class ProjectServerProxy : IServices
    {
        private string host;
        private int port;

        private IObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Org.Example.Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public ProjectServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Org.Example.Response>();
        }

        

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();
                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void sendRequest(Org.Example.Request request)
        {
            lock (stream)
            {
                try
                {

                    request.WriteDelimitedTo(stream);
                    //formatter.Serialize(stream, request);
                    stream.Flush();
                }
                catch (Exception e)
                {
                    throw new Exception("Error sending object " + e);
                }
            }
           

        }

        private Org.Example.Response readResponse()
        {
            Org.Example.Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void initializeConnection()
        {
            try
            {
                Console.WriteLine("Initialize connection");
                Console.WriteLine(host);
                Console.WriteLine(port);

                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                   // object response = formatter.Deserialize(stream);
                    //Org.Example.Request response = (Org.Example.Request)formatter.Deserialize(stream);
                    Org.Example.Response response = Org.Example.Response.Parser.ParseDelimitedFrom(stream);
                    Console.WriteLine("response received " + response);
                    if (response.Type != Org.Example.Response.Types.ResponseType.AddedRezervation)
                    {

                        lock (responses)
                        {
                            responses.Enqueue(response);

                        }
                        _waitHandle.Set();
                    }
                    else
                    {
                        try
                        {
                            client.ReservationMade();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
            
        
        } 

        public bool HandleLogin(string username, string password, IObserver client)
        {
            initializeConnection();
            Org.Example.Request req = new Org.Example.Request {Type = Org.Example.Request.Types.RequestType.Login,Agentie = new Org.Example.Agentie {Id = 1, Username = username, Pass = password } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            if (response.Type == Org.Example.Response.Types.ResponseType.Ok)
            {
                this.client = client;
                return true;
            }
            if (response.Type == Org.Example.Response.Types.ResponseType.Error)
            {
                //string err = response.Data.ToString();

                closeConnection();
                return false;
              
            }
            return false;
        }


        public void Logout(lab8.Domain.Agentie user, IObserver client)
        {
            Console.WriteLine("Logout request...");
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.Logout, Agentie = new Org.Example.Agentie { Id = 1, Username = user.Username, Pass = "" } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            closeConnection();
            if (response.Type == Org.Example.Response.Types.ResponseType.Error)
            {
                throw new Exception("Error logging out" + response.Error);
            }
        }

        public IEnumerable<lab8.Domain.Excursie> GetAllExcursii()
        {
            Console.WriteLine("Get all excursii request ...");
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.GetExcursii };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            
            List<lab8.Domain.Excursie> excursii = new List<lab8.Domain.Excursie>();
            foreach (var excursie in response.Excursii)
            {
                excursii.Add(new lab8.Domain.Excursie(excursie.Id, excursie.ObiectivTuristic, excursie.NumeTransport, excursie.OraPlecare,excursie.Pret,excursie.NrLocuri, excursie.LocuriLibere));
            }
            return excursii;
            
        }

        public IEnumerable<lab8.Domain.Excursie> GetExcursiiBetweenHours(string obiectiv, string ora1, string ora2)
        {
            string oreP = ora1 + "___" + ora2;
            Console.WriteLine("Get excursii ora request ..." + oreP);
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.GetExcursiiOre, Excursie = new Org.Example.Excursie { ObiectivTuristic = obiectiv, OraPlecare = oreP  } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            List<lab8.Domain.Excursie> excursii = new List<lab8.Domain.Excursie>();
            foreach (var excursie in response.Excursii)
            {
                excursii.Add(new lab8.Domain.Excursie(excursie.Id, excursie.ObiectivTuristic, excursie.NumeTransport, excursie.OraPlecare, excursie.Pret, excursie.NrLocuri, excursie.LocuriLibere));
            }
            return excursii;
        }

        public int GetFreeSeats(int id)
        {
            Console.WriteLine("Get free seats request ...");
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.GetFreeSeats, Excursie = new Org.Example.Excursie { Id = id } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            return response.Excursie.LocuriLibere;
        }

        public void AddRezervare(long id, long ex, string nume, string telefon, int nr)
        {
            Console.WriteLine("Add rezervare request ...");
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.AddRez, Rezervare = new Org.Example.Rezervare { Id = id, Excursie = ex, NumeClient = nume, NrTelefon = telefon, NrLocuri = nr } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            

        }

        public long GetId(string username, string password)
        {
            Console.WriteLine("Get id request ...");
            Org.Example.Request req = new Org.Example.Request { Type = Org.Example.Request.Types.RequestType.GetId, Agentie = new Org.Example.Agentie { Id = 1, Username = username, Pass = password } };
            sendRequest(req);
            Org.Example.Response response = readResponse();
            return response.Agentie.Id;
        }

       
    }
}
