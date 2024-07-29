using System;
using System.Net;
using System.Net.Sockets;
using services; // Import your IServices implementation

namespace network 
{
    public class RpcConcurrentServer : AbsConcurrentServer, IDisposable
    {
        private IServices chatServer;
        private TcpListener tcpListener;

        public RpcConcurrentServer(int port, IServices chatServer) : base(port)
        {
            this.chatServer = chatServer;
            Console.WriteLine("Chat- ChatRpcConcurrentServer");
            tcpListener = new TcpListener(System.Net.IPAddress.Any, port);
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            // ChatClientRpcWorker worker = new ChatClientRpcWorker(chatServer, client);
            ClientRpcReflectionWorker worker = new ClientRpcReflectionWorker(chatServer, client);

            Thread workerThread = new Thread(new ThreadStart(worker.Run));
            return workerThread;
        }

        public override void Stop()
        {
            Console.WriteLine("Stopping services ...");
        }

       

        public void Dispose()
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
                tcpListener = null;
            }
        }

        
    }
}
