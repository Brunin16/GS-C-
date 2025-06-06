using Dapper;
using GS_Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Repository
{
    public class EnergyConsumeRepository
    {
        private readonly string _connectionString;

        public EnergyConsumeRepository()
        {
            _connectionString = Config.connectionString;
        }

        public List<EnergyConsume> GetAll()
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"SELECT id, price_per_hour AS PricePerHour, fix_price AS FixPrice,
                               month AS Month, year AS Year, equipament_id AS EquipamentId
                        FROM Energy_Consume";
            return connection.Query<EnergyConsume>(sql).AsList();
        }

        public List<EnergyConsume> GetByEquipamentId(long equipamentId)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"SELECT id, price_per_hour AS PricePerHour, fix_price AS FixPrice,
                               month AS Month, year AS Year, equipament_id AS EquipamentId
                        FROM Energy_Consume
                        WHERE equipament_id = :equipamentId";
            return connection.Query<EnergyConsume>(sql, new { equipamentId }).AsList();
        }

        public EnergyConsume GetById(long id)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"SELECT id, price_per_hour AS PricePerHour, fix_price AS FixPrice,
                               month AS Month, year AS Year, equipament_id AS EquipamentId
                        FROM Energy_Consume
                        WHERE id = :id";
            return connection.QueryFirstOrDefault<EnergyConsume>(sql, new { id });
        }

        public void Insert(EnergyConsume energyConsume)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"INSERT INTO Energy_Consume (price_per_hour, fix_price, month, year, equipament_id)
                        VALUES (:PricePerHour, :FixPrice, :Month, :Year, :EquipamentId)";
            connection.Execute(sql, energyConsume);
        }

        public void Update(EnergyConsume energyConsume)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = @"UPDATE Energy_Consume
                        SET price_per_hour = :PricePerHour, fix_price = :FixPrice, 
                            month = :Month, year = :Year, equipament_id = :EquipamentId
                        WHERE id = :Id";
            connection.Execute(sql, energyConsume);
        }

        public void Delete(long id)
        {
            using var connection = new OracleConnection(_connectionString);
            var sql = "DELETE FROM Energy_Consume WHERE id = :id";
            connection.Execute(sql, new { id });
        }
    }
}
