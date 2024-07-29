using System;
using System.Net.Sockets;
using System.Threading;

namespace network
{
    public abstract class AbsConcurrentServer : AbstractServer
    {
        public AbsConcurrentServer(int port) : base(port)
        {
            Console.WriteLine("Concurrent AbstractServer");
        }

        protected override void ProcessRequest(TcpClient client)
        {
            Thread workerThread = CreateWorker(client);
            workerThread.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}
