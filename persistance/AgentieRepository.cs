using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using lab8.Domain;
using lab8.Repository;

namespace lab8.Repository
{
    public class AgentieRepository : IAgentieRepo
    {
        private string dbConnection;


        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AgentieRepository(string dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public Agentie Delete(long id)
        {
            Agentie ag = null;
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (var command = new SQLiteCommand("select * from Agentie where id=@id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                long idA = reader.GetInt64(0);
                                string username = reader.GetString(1);

                                ag = new Agentie(idA, username);
                            }
                        }
                    }
                    if (ag != null)
                    {
                        using (var command2 = new SQLiteCommand("delete from Agentie where id =@idA", connection))
                        {
                            command2.Parameters.AddWithValue("@idA", ag.Id);
                            command2.ExecuteNonQuery();
                            log.Info($"Agentie with id {id} was successfully deleted.");
                        }
                    }
                    else
                    {
                        log.Warn($"No agentie found with id {id} for deletion.");
                    }
                }
            }
            return ag;
        }

        public List<Agentie> FindAll()
        {
            List<Agentie> agentii = new List<Agentie>();
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie", connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetInt64(0);
                            string username = reader.GetString(1);

                            Agentie ag = new Agentie(id, username);
                            agentii.Add(ag);
                        }
                    }
                }
                //connection.Close();
            }
            log.Info("All agentii retrieved successfully.");
            return agentii;
        }

        public Agentie FindOne(long id)
        {
            Agentie ag = null;
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idA = reader.GetInt64(0);
                            string username = reader.GetString(1);

                            ag = new Agentie(idA, username);
                            log.Info($"Agentie with id {id} was successfully found.");
                        }
                        else
                        {
                            log.Warn($"No agentie found with id {id}.");
                        }
                    }
                }
            }
            return ag;
        }

        public Agentie Save(Agentie entity)
        {
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = new SQLiteCommand("insert into Agentie(username) values (@user)", connection);
                        //command.Parameters.AddWithValue("@id", entity.Id);
                        command.Parameters.AddWithValue("@user", entity.Username);
                        //command.Parameters.AddWithValue("@pass", HashPassword(entity.Password));
                        command.ExecuteNonQuery();

                        transaction.Commit();
                        log.Info($"Agentie with id {entity.Id} was successfully saved.");
                    }
                }
            }
            return entity;


        }

        public Agentie Update(Agentie entity)
        {
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie where id=@id", connection);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var updateCommand = new SQLiteCommand("update Agentie set username = @user where id = @id", connection);
                            updateCommand.Parameters.AddWithValue("@id", entity.Id);
                            updateCommand.Parameters.AddWithValue("@user", entity.Username);
                            //updateCommand.Parameters.AddWithValue("@pass", HashPassword(entity.Password));
                            updateCommand.ExecuteNonQuery();
                            log.Info($"Agentie with id {entity.Id} was successfully updated.");
                            return entity;

                        }
                        else
                        {
                            log.Warn($"No agentie found with id {entity.Id} for update.");
                        }
                    }
                }
            }
            return null;
        }

        public bool loginByUsernamePassword(string username, string password)
        {

            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie where username =@user", connection);
                    command.Parameters.AddWithValue("@user", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idA = reader.GetInt64(0);
                            string username2 = reader.GetString(1);
                            string password2 = reader.GetString(2);
                            if (password2 == HashPassword(password)) return true;
                        }

                    }
                }
            }
            return false;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertim parola într-un array de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertim array-ul de bytes într-un string hexazecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            // Verificăm dacă parola introdusă este aceeași cu cea stocată în baza de date
            string inputHash = HashPassword(inputPassword);
            return string.Equals(inputHash, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

        public void save(string username, string password)
        {
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = new SQLiteCommand("insert into Agentie(username, password) values (@user,@pass)", connection);
                        //command.Parameters.AddWithValue("@id", entity.Id);
                        command.Parameters.AddWithValue("@user", username);
                        command.Parameters.AddWithValue("@pass", HashPassword(password));
                        command.ExecuteNonQuery();

                        transaction.Commit();
                        //log.Info($"Agentie with id {entity.Id} was successfully saved.");
                    }
                }
            }
        }

        public Agentie FindBy(string username, string password)
        {

            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie where username =@user", connection);
                    command.Parameters.AddWithValue("@user", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idA = reader.GetInt64(0);
                            string username2 = reader.GetString(1);
                            string password2 = reader.GetString(2);
                            if (password2 == HashPassword(password)) return new Agentie(idA, username2);
                        }
                    }
                }
            }
            return null;
        }

        public Agentie FindByUser(string username)
        {

            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Agentie where username =@user", connection);
                    command.Parameters.AddWithValue("@user", username);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idA = reader.GetInt64(0);
                            string username2 = reader.GetString(1);
                            string password2 = reader.GetString(2);
                            return new Agentie(idA, username2);
                        }
                    }
                }
            }
            return null;
        }


    }
}
