using System;
using client;
using services;
using server;
using network;
using Microsoft.VisualBasic.ApplicationServices;
using networking;


namespace client
{
    public class StartClient
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);
            //IServices server = new ServicesRpcProxy("127.0.0.1", 55555);
            IServices server = new ProjectServerProxy("127.0.0.1", 55555);
            ClientCtrl client = new ClientCtrl(server);
            Login login = new Login(client);
            login.setServer(server);
            Application.Run(login);

        }
    }
}