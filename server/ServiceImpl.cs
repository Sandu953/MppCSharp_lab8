using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab8.Repository;
using lab8.Domain;
using services;



namespace server
{
    public class ServicesImpl : IServices
    {
        private AgentieRepository agentieRepo;
        private ExcursieRepository excursieRepo;
        private RezervareRepository rezervareRepo;
        private ConcurrentDictionary<long, IObserver> loggedClients;

        public ServicesImpl(AgentieRepository agentieRepo, ExcursieRepository excursieRepo, RezervareRepository rezervareRepo)
        {
            this.agentieRepo = agentieRepo;
            this.excursieRepo = excursieRepo;
            this.rezervareRepo = rezervareRepo;
            loggedClients = new ConcurrentDictionary<long, IObserver>();
        }

        public bool HandleLogin(string username, string password, IObserver client)
        {
            Agentie agentie = agentieRepo.FindBy(username, password);
            if (agentie != null)
            {
                if (loggedClients.ContainsKey(agentie.Id))
                    return false;

                loggedClients.TryAdd(agentie.Id, client);
            }
            else
            {
                return false;
            }

            return agentieRepo.loginByUsernamePassword(username, password);
        }

        public long GetId(string username, string password)
        {
            Agentie ag = agentieRepo.FindBy(username, password);
            return ag.Id;
        }

        public void Logout(Agentie user, IObserver client)
        {
            Agentie ag = agentieRepo.FindByUser(user.Username);
            if (ag == null)
                throw new Exception("User " + user.Id + " is not logged in.");

            if (!loggedClients.TryRemove(ag.Id, out IObserver localClient))
                throw new Exception("User " + user.Id + " is not logged in.");
        }

        public IEnumerable<Excursie> GetAllExcursii()
        {
            return excursieRepo.FindAll();
        }

        public IEnumerable<Excursie> GetExcursiiBetweenHours(string obiectiv, string ora1, string ora2)
        {
            Console.WriteLine("ServiceImpl: " + obiectiv + " " + ora1 + " " + ora2);
            return excursieRepo.findBeetwenHours(ora1, ora2, obiectiv);
        }

        public int GetFreeSeats(int id)
        {
            return excursieRepo.findLocuriLibere(id);
        }

        public void AddRezervare(long id, long ex, string nume, string telefon, int bilet)
        {
            try
            {
                Rezervare rez = new Rezervare(id,ex, nume, telefon, bilet);
                rezervareRepo.Save(rez);
                Console.WriteLine("Notifying");
                NotifyRezervare(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private const int DefaultThreadsNo = 5;

        private void NotifyRezervare(long id)
        {
            var tasks = new List<Task>();
            foreach (var entry in loggedClients)
            {
                Console.WriteLine("AAA" + entry.Key);
                if (entry.Key != id)
                {
                    IObserver chatClient = entry.Value;
                    if (chatClient != null)
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            try
                            {
                                Task.Run(() => chatClient.ReservationMade());
                            }
                            catch (Exception e)
                            {
                                Console.Error.WriteLine("Error notifying " + e);
                            }
                        }));
                    }
                }
            }
            Task.WhenAll(tasks);
        }

       
    }
}
