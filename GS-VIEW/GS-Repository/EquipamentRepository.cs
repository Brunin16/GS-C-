using GS_Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Repository
{
    public class EquipamentRepository
    {
        private readonly string _connectionString;

        public EquipamentRepository()
        {
            _connectionString = Config.connectionString;
        }

        public void Create(Equipament equipament)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand(@"
                INSERT INTO Equipament (name, hour_used_per_day, person_id) 
                VALUES (:name, :hour_used_per_day, :person_id)", connection);

                command.Parameters.Add(new OracleParameter("name", equipament.Name));
                command.Parameters.Add(new OracleParameter("hour_used_per_day", equipament.HourUsedPerDay));
                command.Parameters.Add(new OracleParameter("person_id", equipament.PersonId));

                command.ExecuteNonQuery();
            }
        }

        public Equipament GetById(long id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("SELECT * FROM Equipament WHERE id = :id", connection);
                command.Parameters.Add(new OracleParameter("id", id));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Equipament
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            HourUsedPerDay = reader.GetDouble(reader.GetOrdinal("hour_used_per_day")),
                            PersonId = reader.GetInt64(reader.GetOrdinal("person_id"))
                        };
                    }
                    return null;
                }
            }
        }

        public List<Equipament> ListAll()
        {
            var list = new List<Equipament>();
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("SELECT * FROM Equipament", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Equipament
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            HourUsedPerDay = reader.GetDouble(reader.GetOrdinal("hour_used_per_day")),
                            PersonId = reader.GetInt64(reader.GetOrdinal("person_id"))
                        });
                    }
                }
            }
            return list;
        }

        public void Update(Equipament equipament)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand(@"
                UPDATE Equipament 
                SET name = :name, hour_used_per_day = :hour_used_per_day, person_id = :person_id
                WHERE id = :id", connection);

                command.Parameters.Add(new OracleParameter("name", equipament.Name));
                command.Parameters.Add(new OracleParameter("hour_used_per_day", equipament.HourUsedPerDay));
                command.Parameters.Add(new OracleParameter("person_id", equipament.PersonId));
                command.Parameters.Add(new OracleParameter("id", equipament.Id));

                command.ExecuteNonQuery();
            }
        }

        public void Delete(long id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("DELETE FROM Equipament WHERE id = :id", connection);
                command.Parameters.Add(new OracleParameter("id", id));
                command.ExecuteNonQuery();
            }
        }
    }
}