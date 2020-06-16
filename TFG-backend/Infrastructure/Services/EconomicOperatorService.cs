using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EconomicOperatorService : IEconomicOperatorService
    {
        private readonly DatabaseFacade _database;

        public EconomicOperatorService(DBContext context)
        {
            _database = context.Database;
        }

        public async Task<List<EconomicOperators>> GetEOID(string EOID)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT *
                            FROM EconomicOperators EO";

            List<EconomicOperators> result = connection.Query<EconomicOperators>(sql) != null ? connection.Query<EconomicOperators>(sql).ToList() : new List<EconomicOperators>();

            connection.Close();
            return result;
        }

        public async Task<List<string>> GetEOIDS()
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT EO.Id
                            FROM EconomicOperators EO";

            List<string> result = connection.Query<string>(sql) != null ? connection.Query<string>(sql).ToList() : new List<string>();

            connection.Close();
            return result;
        }

        public async Task<string> DeleteEO(string id)
        {
            DbConnection connection = GetConnection();

            string sql = @" DELETE FROM EconomicOperators WHERE Id= '" + id + "'";

            var result = connection.Query<string>(sql) != null ? connection.Query<string>(sql).FirstOrDefault() :  string.Empty;

            connection.Close();
            return result;
        }

        private DbConnection GetConnection()
        {
            var connection = _database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public async Task<string> GetEOById(string id)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT EO.Id
                            FROM EconomicOperators EO
                            WHERE EO.Id ='"+ id + "'";

            var result = connection.Query<string>(sql) != null ? connection.Query<string>(sql).FirstOrDefault() : string.Empty;

            connection.Close();
            return result;
        }

        public async Task<EconomicOperators> GetEO(string id)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT *
                            FROM EconomicOperators EO
                            WHERE EO.Id ='" + id + "'";

            var result = connection.Query<EconomicOperators>(sql) != null ? connection.Query<EconomicOperators>(sql).FirstOrDefault() : new EconomicOperators();

            connection.Close();
            return result;
        }
    }
}
