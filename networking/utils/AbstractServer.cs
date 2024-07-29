using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace network 
{
    public abstract class AbstractServer
    {
        private int port;
        private TcpListener server = null;

        public AbstractServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                Console.WriteLine("Server started on port " + port);
                while (true)
                {
                    Console.WriteLine("Waiting for clients ...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected ...");
                    ProcessRequest(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Starting server error: " + e.Message);
            }
            finally
            {
                Stop();
            }
        }

        protected abstract void ProcessRequest(TcpClient client);

        public virtual void Stop()
        {
            try
            {
                if (server != null)
                {
                    server.Stop();
                    Console.WriteLine("Server stopped.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Stopping server error: " + e.Message);
            }
        }
    }
}
