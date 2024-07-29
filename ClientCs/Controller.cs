using System;
using System.Collections.Generic;
using lab8.Domain;
using lab8.Repository;
using Microsoft.VisualBasic.ApplicationServices;
using services;

namespace client
{
    public class ClientCtrl : IObserver
    {
        //public event EventHandler<UserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IServices server;
        private Agentie currentUser;

        public ClientCtrl(IServices server)
        {
            this.server = server;
            currentUser = null;
        }

        public bool login(string username, string password)
        {
            if (server.HandleLogin(username, password, this))
            {
                Agentie user = new Agentie(1,username);
                currentUser = user;
                return true;
            }     
            return false;
        }

        public void ReservationMade()
        {
            throw new NotImplementedException();
        }
    }
}