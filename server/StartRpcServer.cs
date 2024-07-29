using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using lab8.Repository;
using server;
using services;
using network;
using System.Net.Sockets;
using networking;

namespace server 
{
    class StartRpcServer
    {
        private static int defaultPort = 55555;

        static void Main(string[] args)
        {
            // UserRepository agentieRepo = new UserRepositoryMock();
            //Dictionary<string, string> serverProps = new Dictionary<string, string>();
            //try
            //{
            //    foreach (var line in File.ReadLines("C:\\Users\\probook\\source\\repos\\server\\chatserver.properties"))
            //    {
            //        var parts = line.Split('=');
            //        serverProps[parts[0]] = parts[1];
            //    }
            //    Console.WriteLine("Server properties set.");
            //    foreach (var prop in serverProps)
            //    {
            //        Console.WriteLine($"{prop.Key}: {prop.Value}");
            //    }
            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine("Cannot find chatserver.properties " + e);
            //    return;
            //}
            

            AgentieRepository agentieRepo = new AgentieRepository("Data Source=C:/Users/probook/Desktop/EXAMEN MAP/mpp-proiect-java-Sandu953/FirmaExcursie.db;Version=3;");
            //AgentieRepository agentieORMRepo = new AgentieRepository(new AppDbContext());
            ExcursieRepository excursieRepo = new ExcursieRepository("Data Source=C:/Users/probook/Desktop/EXAMEN MAP/mpp-proiect-java-Sandu953/FirmaExcursie.db;Version=3;");
            RezervareRepository rezervareRepo = new RezervareRepository("Data Source=C:/Users/probook/Desktop/EXAMEN MAP/mpp-proiect-java-Sandu953/FirmaExcursie.db;Version=3;", excursieRepo);
            IServices serverImpl = new ServicesImpl(agentieRepo, excursieRepo, rezervareRepo);

            int serverPort = defaultPort;
            //if (!int.TryParse(serverProps["chat.server.port"], out serverPort))
            //{
            //    Console.WriteLine("Wrong Port Number");
            //    Console.WriteLine("Using default port " + defaultPort);
            //    serverPort = defaultPort;
            //}

            Console.WriteLine("Starting server on port: " + serverPort);
            //SerialProjectServer server = new SerialProjectServer( serverImpl);
            // ProjectServerProxy serverProxy = new ProjectServerProxy()
            RPCConcurrentServerProto serverProxy = new RPCConcurrentServerProto("127.0.0.1", 55555, serverImpl);
            serverProxy.Start();
            Console.ReadLine();

            //using (RpcConcurrentServer server = new RpcConcurrentServer(serverPort, serverImpl))
            //{
            //    try
            //    {
            //        server.Start();
            //        Task.Delay(-1).Wait(); // Keep the server running until manually stopped
            //    }
            //    catch (ServerException e)
            //    {
            //        Console.WriteLine("Error starting the server" + e.Message);
            //    }
            //    finally
            //    {
            //        try
            //        {
            //            server.Stop();
            //        }
            //        catch (ServerException e)
            //        {
            //            Console.WriteLine("Error stopping server " + e.Message);
            //        }
            //    }
            //}
            //using (ConcurrentServerProto server = new ConcurrentServerProto(serverPort, serverImpl))
            //{
            //    try
            //    {
            //        server.Start();
            //        Task.Delay(-1).Wait(); // Keep the server running until manually stopped
            //    }
            //    catch (ServerException e)
            //    {
            //        Console.WriteLine("Error starting the server" + e.Message);
            //    }
            //    finally
            //    {
            //        try
            //        {
            //            server.Stop();
            //        }
            //        catch (ServerException e)
            //        {
            //            Console.WriteLine("Error stopping server " + e.Message);
            //        }
            //    }
            //}
        }

        //public class SerialProjectServer : ConcurrentServerProto
        //{
        //    private IServices server;
        //    private ProjectProtoWorker worker;
        //    public SerialProjectServer(string host, int port, IServices server) : base(host, port)
        //    {
        //        this.server = server;
        //        Console.WriteLine("SerialProjectServer...");
        //    }
        //    protected override Thread createWorker(TcpClient client)
        //    {
        //        worker = new ProjectProtoWorker(server, client);
        //        return new Thread(new ThreadStart(worker.run));
        //    }
        //}
    }
}