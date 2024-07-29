using lab8.Domain;
using services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace services 
{
    public interface IServices
    {
        bool HandleLogin(string username, string password, IObserver client);
        void Logout(Agentie user, IObserver client);
        IEnumerable<Excursie> GetAllExcursii();
        IEnumerable<Excursie> GetExcursiiBetweenHours(string obiectiv, string ora1, string ora2);
        int GetFreeSeats(int id);
        void AddRezervare(long id, long ex, string nume, string telefon, int bilet);
        long GetId(string username, string password);
    }
}