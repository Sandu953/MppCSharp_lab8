using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8.Domain;
using lab8.Repository;
using log4net.Repository.Hierarchy;

namespace lab8.Repository
{
	public class ExcursieRepository : IExcursieRepo
	{
		private string dbConnection;

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public ExcursieRepository(string dbConnection)
		{
			this.dbConnection = dbConnection;
		}

		public Excursie Delete(long id)
		{
			Excursie ex = null;
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();
				if (connection.State == System.Data.ConnectionState.Open)
				{
					using (var command = new SQLiteCommand("SELECT Excursie.*, (Excursie.numar_locuri - COALESCE(SUM(Rezervare.numar_locuri), 0)) AS locuri_libere FROM Excursie LEFT JOIN Rezervare ON Excursie.id = Rezervare.excursie WHERE Excursie.id=? GROUP BY Excursie.id", connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								long idA = reader.GetInt64(0);
								string obiectiv = reader.GetString(1);
								string firma = reader.GetString(2);
								string timeString = reader.GetString(3);
								
								int pret = reader.GetInt32(4);
								int nrLocuri = reader.GetInt32(5);
								int nrLocuriLibere = reader.GetInt32(6);
								ex = new Excursie(idA,obiectiv,firma,timeString,pret, nrLocuri,nrLocuriLibere);
							}
						}
					}
					if (ex != null)
					{
						using (var command2 = new SQLiteCommand("delete from Excursie where id =@idA", connection))
						{
							command2.Parameters.AddWithValue("@idA", ex.Id);
							command2.ExecuteNonQuery();
							log.Info($"Excursie with id {id} was successfully deleted.");
						}
						
					}
					else
					{
						log.Warn($"No excursie found with id {id} for deletion.");
					}
				}
			}
			return ex;
		}

		public List<Excursie> FindAll()
		{
			List<Excursie> excursii = new List<Excursie>();
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();

				if (connection.State == System.Data.ConnectionState.Open)
				{
					var command = new SQLiteCommand("SELECT Excursie.*, (Excursie.numar_locuri - COALESCE(SUM(Rezervare.numar_locuri), 0)) AS locuri_libere FROM Excursie LEFT JOIN Rezervare ON Excursie.id = Rezervare.excursie GROUP BY Excursie.id", connection);
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							long id = reader.GetInt64(0);
							string obiectiv = reader.GetString(1);
							string firma = reader.GetString(2);
                            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(3)).LocalDateTime;
                            string timeString = dateTime.ToString("HH:mm:ss");
							int pret = reader.GetInt32(4);
							int nrLocuri = reader.GetInt32(5);
							int nrLocuriLibere = reader.GetInt32(6);
							Excursie ex = new Excursie(id, obiectiv, firma, timeString, pret, nrLocuri, nrLocuriLibere);
							excursii.Add(ex);
						}
					}
				}
			}
			log.Info("All excursii retrieved successfully.");
			return excursii;
		}

		public Excursie FindOne(long id)
		{
			Excursie ex = null;
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();
				if (connection.State == System.Data.ConnectionState.Open)
				{
					var command = new SQLiteCommand("SELECT Excursie.*, (Excursie.numar_locuri - COALESCE(SUM(Rezervare.numar_locuri), 0)) AS locuri_libere FROM Excursie LEFT JOIN Rezervare ON Excursie.id = Rezervare.excursie WHERE Excursie.id=? GROUP BY Excursie.id", connection);
					command.Parameters.AddWithValue("@id", id);
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							long idA = reader.GetInt64(0);
							string obiectiv = reader.GetString(1);
							string firma = reader.GetString(2);
                            //string timeString = reader.GetString(3);
                            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(3)).LocalDateTime;
                            string timeString = dateTime.ToString("HH:mm:ss");
                            int pret = reader.GetInt32(4);
							int nrLocuri = reader.GetInt32(5);
							int nrLocuriLibere = reader.GetInt32(6);
							ex = new Excursie(idA, obiectiv, firma, timeString, pret, nrLocuri, nrLocuriLibere);
							log.Info($"Agentie with id {id} was successfully found.");
						}
						else
						{
							log.Warn($"No excursie found with id {id}.");
						}
					}
				}
			}
			return ex;
		}

		public Excursie Save(Excursie entity)
		{
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();
				if (connection.State == System.Data.ConnectionState.Open)
				{
					using (var transaction = connection.BeginTransaction())
					{
						var command = new SQLiteCommand("insert into Excursie(obiectiv,firma_transport,ora_plecare,pret,numar_locuri) values (@obiectiv,@firma,@ora,@pret,@numar)", connection);
						command.Parameters.AddWithValue("@obiectiv", entity.ObiectivTuristic);
						command.Parameters.AddWithValue("@firma", entity.NumeTransport);
						command.Parameters.AddWithValue("@ora", DateTime.ParseExact(entity.OraPlecare,"HH:mm:ss",  CultureInfo.InvariantCulture));
						command.Parameters.AddWithValue("@pret", entity.Pret);
						command.Parameters.AddWithValue("@numar", entity.NrLocuri);

						command.ExecuteNonQuery();

						transaction.Commit();
					}
				}
			}
			log.Info("Excursie was successfully saved.");
			return entity;


		}

		public Excursie Update(Excursie entity)
		{
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();
				if (connection.State == System.Data.ConnectionState.Open)
				{
					var command = new SQLiteCommand("select * from Excursie where id=@id", connection);
					command.Parameters.AddWithValue("@id", entity.Id);
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							var updateCommand = new SQLiteCommand("update Excursie set obiectiv = @obiect, firma_transport = @firma, ora_plecare = @ora, pret = @pret, numar_locuri = @numar where id = @id", connection);
							updateCommand.Parameters.AddWithValue("@id", entity.Id);
							updateCommand.Parameters.AddWithValue("@obiect", entity.ObiectivTuristic);
							updateCommand.Parameters.AddWithValue("@firma", entity.NumeTransport);
							updateCommand.Parameters.AddWithValue("@ora", DateTime.ParseExact(entity.OraPlecare, "HH:mm:ss", CultureInfo.InvariantCulture));
							updateCommand.Parameters.AddWithValue("@pret", entity.Pret);
							updateCommand.Parameters.AddWithValue("@numar", entity.NrLocuri);
							updateCommand.ExecuteNonQuery();
							log.Info($"Excursie with id {entity.Id} was successfully updated.");
							return entity;
						}
						else
						{
							log.Warn($"No excursie found with id {entity.Id} for update.");
						}
					}
				}
			}
			return null;
		}

        public List<Excursie> findBeetwenHours(string ora1, string ora2, string obiectiv)
        {
			List<Excursie> excursii = new List<Excursie>();
			using (var connection = new SQLiteConnection(dbConnection))
			{
				connection.Open();

				if (connection.State == System.Data.ConnectionState.Open)
				{
					var command = new SQLiteCommand("SELECT Excursie.*, (Excursie.numar_locuri - COALESCE(SUM(Rezervare.numar_locuri), 0)) AS locuri_libere FROM Excursie LEFT JOIN Rezervare ON Excursie.id = Rezervare.excursie WHERE ora_plecare BETWEEN ? AND ? AND obiectiv = ? GROUP BY Excursie.id", connection);
					command.Parameters.AddWithValue("@ora1", ora1);
					command.Parameters.AddWithValue("@ora2", ora2);
					command.Parameters.AddWithValue("@obiectiv", obiectiv);
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
                        int locuriLibereIndex = reader.GetOrdinal("locuri_libere");
                        while (reader.Read())
						{
							long id = reader.GetInt64(0);
      
                            string obiectiv2 = reader.GetString(1);
                      
                            string firma = reader.GetString(2);
                           
                            DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64(3)).LocalDateTime;
                            string timeString = dateTime.ToString("HH:mm:ss");
							
                            int pret = reader.GetInt32(4);
							
							int nrLocuri = reader.GetInt32(5);
							
                            int nrLocuriLibere = reader.GetInt32(locuriLibereIndex);
							
                            Excursie ex = new Excursie(id, obiectiv2, firma, timeString, pret, nrLocuri, nrLocuriLibere);
							excursii.Add(ex);
						}
					}
				}
			}
			log.Info("All excursii retrieved successfully.");
			return excursii;
		}

		public int findLocuriLibere(int id)
		{
			int locuriLibere = 0;
            using (var connection = new SQLiteConnection(dbConnection))
            {
                connection.Open();
				int locuriOcupate = 0;
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    var command = new SQLiteCommand("SELECT numar_locuri FROM Rezervare WHERE excursie=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int locuri = reader.GetInt32(0);
							locuriOcupate += locuri;
                        }

						var command2 = new SQLiteCommand("SELECT numar_locuri FROM Excursie WHERE id=@id", connection);
						command2.Parameters.AddWithValue("@id", id);
						using (SQLiteDataReader reader2 = command2.ExecuteReader())
						{
							if (reader2.Read())
							{
                                int locuri = reader2.GetInt32(0);
                                locuriLibere = locuri - locuriOcupate;
                            }
						}
                    }
                }
            }
			return locuriLibere;
        }
    }
}
