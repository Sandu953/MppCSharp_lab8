//using System;
//using System.Data.Entity;
//using lab8.Domain;

//namespace lab8.Repository
//{
//    public class AgentieRepository : IAgentieRepo
//    {

//        private readonly AppDbContext dbContext;

//        public AgentieRepository(AppDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }

//        public List<Agentie> FindAll()
//        {
//            return dbContext.Agenties.ToList();
//        }

//        public Agentie FindOne(long id)
//        {
//            return dbContext.Agenties.FirstOrDefault(u => u.Id == id);
//        }

//        public Agentie Save(Agentie entity)
//        {
//            dbContext.Agenties.Add(entity);
//            dbContext.SaveChanges();
//            return entity;
//        }

//        public Agentie Delete(long id)
//        {
//            var Agentie = dbContext.Agenties.FirstOrDefault(u => u.Id == id);
//            if (Agentie != null)
//            {
//                dbContext.Agenties.Remove(Agentie);
//                dbContext.SaveChanges();
//            }
//            return Agentie;
//        }

//        public Agentie Update(Agentie entity)
//        {
//            return null;
//        }

//        public bool loginByUsernamePassword(string username, string password)
//        {
//           // throw new NotImplementedException();
//            var hashedPassword = AgentieRepository.HashPassword(password);
//            return dbContext.Agenties.Any(a => a.Username == username && a.Password == hashedPassword);
//            //return dbContext.Agenties.FirstOrDefault(a => a.Username == username && a.Password == hashedPassword);
//        }

//        public void save(string username, string password)
//        {
//            throw new NotImplementedException();
//        }

//        public Agentie FindByUser(string username)
//        {
//            return dbContext.Agenties.FirstOrDefault(a => a.Username == username);
//        }

//        public Agentie FindBy(string username, string password)
//        {
//            var hashedPassword = HashPassword(password);
//            return dbContext.Agenties.FirstOrDefault(a => a.Username == username && a.Password == hashedPassword);
//        }

//        public static string HashPassword(string password)
//        {
//            using var sha256Hash = System.Security.Cryptography.SHA256.Create();
//            var bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
//            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
//        }

//        public static bool VerifyPassword(string inputPassword, string hashedPassword)
//        {
//            var inputHash = AgentieRepository.HashPassword(inputPassword);
//            return string.Equals(inputHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
//        }
//    }
//}