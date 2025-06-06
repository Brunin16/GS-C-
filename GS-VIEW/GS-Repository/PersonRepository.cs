using GS_Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Repository
{
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository()
        {
            _connectionString = Config.connectionString;
        }

        public void Add(Person person)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("INSERT INTO Person (full_Name, email, cpf, years, endress, country) VALUES (:fullName, :email, :cpf, :years, :endress, :country)", connection);
                command.Parameters.Add(new OracleParameter("fullName", person.FullName));
                command.Parameters.Add(new OracleParameter("email", person.Email));
                command.Parameters.Add(new OracleParameter("cpf", person.Cpf));
                command.Parameters.Add(new OracleParameter("years", person.Years));
                command.Parameters.Add(new OracleParameter("endress", person.Endress));
                command.Parameters.Add(new OracleParameter("country", person.Country));
                command.ExecuteNonQuery();
            }
        }

        public Person GetById(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("SELECT * FROM Person WHERE id = :id", connection);
                command.Parameters.Add(new OracleParameter("id", id));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Person
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FullName = reader["full_Name"].ToString(),
                            Email = reader["email"].ToString(),
                            Cpf = reader["cpf"].ToString(),
                            Years = Convert.ToInt32(reader["years"]),
                            Endress = reader["endress"].ToString(),
                            Country = reader["country"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void Delete(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("DELETE FROM Person WHERE id = :id", connection);
                command.Parameters.Add(new OracleParameter("id", id));
                command.ExecuteNonQuery();
            }
        }

        public List<Person> ListAll()
        {
            var people = new List<Person>();

            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand("SELECT * FROM Person", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        people.Add(new Person
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FullName = reader["full_Name"].ToString(),
                            Email = reader["email"].ToString(),
                            Cpf = reader["cpf"].ToString(),
                            Years = Convert.ToInt32(reader["years"]),
                            Endress = reader["endress"].ToString(),
                            Country = reader["country"].ToString()
                        });
                    }
                }
            }

            return people;
        }
        public void Update(Person person)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                var command = new OracleCommand(@"
                    UPDATE Person 
                    SET fullName = :full_Name, 
                        email = :email, 
                        cpf = :cpf, 
                        years = :years, 
                        endress = :endress, 
                        country = :country
                    WHERE id = :id", connection);

                command.Parameters.Add(new OracleParameter("full_Name", person.FullName));
                command.Parameters.Add(new OracleParameter("email", person.Email));
                command.Parameters.Add(new OracleParameter("cpf", person.Cpf));
                command.Parameters.Add(new OracleParameter("years", person.Years));
                command.Parameters.Add(new OracleParameter("endress", person.Endress));
                command.Parameters.Add(new OracleParameter("country", person.Country));
                command.Parameters.Add(new OracleParameter("id", person.Id));

                command.ExecuteNonQuery();
            }
        }

    }
}
