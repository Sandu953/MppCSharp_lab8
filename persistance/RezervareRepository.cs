using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;

using System.Threading.Tasks;
using lab8.Domain;

namespace lab8.Repository
{
    public class RezervareRepository : IRepository<long, Rezervare>
    {
        private string dbConnection;
        private ExcursieRepository repo;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public RezervareRepository(string dbConnection, ExcursieRepository repo)
        {
            this.dbConnection = dbConnection;
            this.repo = repo;
        }

        public Rezervare Delete(long id)
        {
            Rezervare rez = null;
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (var command = new SQLiteCommand("select * from Rezervare where id=@id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                long idA = reader.GetInt64(0);
                                long idE = reader.GetInt64(1);
                                string numeClient = reader.GetString(2);
                                string nrTelefon = reader.GetString(3);
                                int nrLocuri = reader.GetInt32(4);
                                rez = new Rezervare(idA, idE, numeClient, nrTelefon, nrLocuri);
                            }
                        }
                    }
                    if (rez != null)
                    {
                        using (var command2 = new SQLiteCommand("delete from Rezervare where id =@idA", connection))
                        {
                            command2.Parameters.AddWithValue("@idA", rez.Id);
                            command2.ExecuteNonQuery();
                            log.Info($"Rezervare with id {id} was successfully deleted.");
                        }
                    }
                    else
                    {
                        log.Warn($"No rezervare found with id {id} for deletion.");
                    }
                }
            }
            return rez;
        }

        public List<Rezervare> FindAll()
        {
            List<Rezervare> rezervari = new List<Rezervare>();
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Rezervare", connection);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetInt64(0);
                            long idE = reader.GetInt64(1);
                            string numeClient = reader.GetString(2);
                            string nrTelefon = reader.GetString(3);
                            int nrLocuri = reader.GetInt32(4);
                            Rezervare rez = new Rezervare(id, idE, numeClient, nrTelefon, nrLocuri);
                            rezervari.Add(rez);
                        }
                    }
                }
            }
            log.Info("Returned all rezervari.");
            return rezervari;
        }

        public Rezervare FindOne(long id)
        {
            Rezervare rez = null;
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Rezervare where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long idA = reader.GetInt64(0);
                            long idE = reader.GetInt64(1);
                            string numeClient = reader.GetString(2);
                            string nrTelefon = reader.GetString(3);
                            int nrLocuri = reader.GetInt32(4);
                            rez = new Rezervare(id, idE, numeClient, nrTelefon, nrLocuri);
                            log.Info($"Rezervare with id {id} was successfully found.");
                        }
                        else
                        {
                            log.Info($"No rezervare found with id {id}.");
                        }
                    }
                }
            }
            return rez;
        }

        public Rezervare Save(Rezervare entity)
        {
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = new SQLiteCommand("insert into Rezervare(excursie,nume_client,numar_telefon,numar_locuri) values (@excursie, @nume_client, @numar_telefon, @numar_locuri)", connection);
                        command.Parameters.AddWithValue("@excursie", entity.Excursie);
                        command.Parameters.AddWithValue("@nume_client", entity.NumeClient);
                        command.Parameters.AddWithValue("@numar_telefon", entity.NrTelefon);
                        command.Parameters.AddWithValue("@numar_locuri", entity.NrLocuri);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
            }
            return entity;
        }

        public Rezervare Update(Rezervare entity)
        {
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("select * from Rezervare where id=@id", connection);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var updateCommand = new SQLiteCommand("update Rezervare set excursie = @ex, nume_client = @nume, numar_telefon = @tel, numar_locuri = @numar where id = @id", connection);
                            updateCommand.Parameters.AddWithValue("@id", entity.Id);
                            updateCommand.Parameters.AddWithValue("@ex", entity.Excursie);
                            updateCommand.Parameters.AddWithValue("@nume", entity.NumeClient);
                            updateCommand.Parameters.AddWithValue("@tel", entity.NrTelefon);
                            updateCommand.Parameters.AddWithValue("@numar", entity.NrLocuri);

                            updateCommand.ExecuteNonQuery();
                            return entity;
                        }
                    }
                }
            }
            return null;
        }
    }
}
