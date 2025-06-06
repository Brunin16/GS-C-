using Dapper;
using GS_Model.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Repository
{
    public class AppUserRepository
    {
        private readonly string _connectionString;

        public AppUserRepository()
        {
            _connectionString = Config.connectionString;
        }

        public List<AppUser> GetAll()
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = "SELECT id, username, password, person_id FROM App_User";
            return connection.Query<AppUser>(sql).ToList();
        }
        public bool validarUsuario(string usuario, string senha)
        {
            using (OracleConnection conexao = new OracleConnection(_connectionString))
            {
                conexao.Open();
                string query = $"select count(1) from App_User where username = '{usuario}' and PASSWORD = '{senha}'";
                OracleCommand cmd = new OracleCommand(query, conexao);

                object retorno = cmd.ExecuteScalar();

                if (int.Parse(retorno.ToString()) != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public AppUser GetByUsername(string username)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT * FROM App_User WHERE username = :username", connection))
                {
                    command.Parameters.Add(new OracleParameter("username", username));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AppUser
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Username = reader["username"].ToString(),
                                Password = reader["password"].ToString(),
                                PersonId = reader["person_id"] == DBNull.Value
                                    ? null
                                    : Convert.ToInt64(reader["person_id"])
                            };
                        }
                    }
                }
            }

            return null;
        }


        public bool Login(string username, string password)
        {
            using var connection = new OracleConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM APP_USER 
                            WHERE USERNAME = :username AND PASSWORD = :password";
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("password", password));

            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }


        public AppUser GetById(long id)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = "SELECT id, username, password, person_id FROM App_User WHERE id = :id";
            return connection.QueryFirstOrDefault<AppUser>(sql, new { id });
        }

        public void Insert(AppUser user)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"INSERT INTO App_User (username, password, person_id) 
                VALUES (:username, :password, :person_id)";

            var parameters = new DynamicParameters();
            parameters.Add("username", user.Username);
            parameters.Add("password", user.Password);

            if (user.PersonId.HasValue)
                parameters.Add("person_id", user.PersonId.Value, DbType.Int64);
            else
                parameters.Add("person_id", null, DbType.Int64); 

            connection.Execute(sql, parameters);
        }




        public void Update(AppUser user)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"UPDATE App_User 
                SET username = :username, password = :password, person_id = :person_id 
                WHERE id = :id";

            var dapperParams = new Dapper.DynamicParameters();
            dapperParams.Add("id", user.Id);
            dapperParams.Add("username", user.Username);
            dapperParams.Add("password", user.Password);
            dapperParams.Add("person_id", user.PersonId.HasValue ? user.PersonId.Value : DBNull.Value);

            connection.Execute(sql, dapperParams);
        }


        public void Delete(long id)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = "DELETE FROM App_User WHERE id = :id";
            connection.Execute(sql, new { id });
        }
    }
}
