using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Filters;

namespace Infrastructure.Services
{
    public class SerialService : ISerialService
    {
        private readonly DatabaseFacade _database;

        public SerialService(DBContext context)
        {
            _database = context.Database;
        }

        private DbConnection GetConnection()
        {
            var connection = _database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return connection;
        }

        public async Task<int> GetTotalSerials(SerialFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT COUNT(*)
                            FROM Serials S 
                            WHERE " + filterStr;

            var result = connection.Query<int>(sql).FirstOrDefault();
            connection.Close();
            return result;
        }

        public async Task<List<Serials>> GetSerials(SerialFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT * 
                            FROM Serials S
                            WHERE " + filterStr;

            var result = connection.Query<Serials>(sql).ToList();
            
            foreach(var s in result)
            {
                string sql2 = @" SELECT * 
                            FROM ArrivalSerials ASE
                            WHERE ASE.Serial= '" + s.Serial +"'";
                s.ArrivalSerials = connection.Query<ArrivalSerials>(sql2).ToList();

                string sql3 = @" SELECT * 
                            FROM DispatchSerials DS
                            WHERE DS.Serial= '" + s.Serial + "'";
                s.DispatchSerials = connection.Query<DispatchSerials>(sql3).ToList();

                string sql4 = @" SELECT * 
                            FROM InvoiceSerials ISE
                            WHERE ISE.Serial= '" + s.Serial + "'";
                s.InvoiceSerials = connection.Query<InvoiceSerials>(sql4).ToList();

                string sql5 = @" SELECT * 
                            FROM RequestSerials RS
                            WHERE RS.Serial= '" + s.Serial + "'";
                s.RequestSerials = connection.Query<RequestSerials>(sql5).ToList();
            }

            connection.Close();
            return result;
        }

        private string GetFilter(SerialFilter filter)
        {
            string filterStr = "1=1";

            if (filter.Id != 0)
            {
                filterStr += $" AND S.Id = {filter.Id}";
            }

            if (!string.IsNullOrEmpty(filter.Serial))
            {
                filterStr += $" AND S.Serial like '{filter.Serial}%'";
            }

            return filterStr;
        }

        public async Task<int> GetLastIdSerial()
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT ISNULL(MAX(S.Id),0) AS Id FROM Serials AS S";

            var result = connection.Query<int>(sql).ToList();

            var lastId = result.Any() ? result.FirstOrDefault() : 0;

            connection.Close();
            return lastId;
        }

        public async Task<Serials> GetSerialBySerial(string serial)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT S.Id AS Id 
                            FROM Serials AS S
                            WHERE S.Serial = '" + serial + "'";

            var result = connection.Query<Serials>(sql);

            var serialResult = result.Any() ? result.FirstOrDefault() : null;

            connection.Close();
            return serialResult;
        }

        public async Task<Serials> GetSerialById(int id)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT S.Id AS Id 
                            FROM Serials AS S
                            WHERE S.Id = '" + id + "'";

            var result = connection.Query<Serials>(sql);

            var serialResult = result.Any() ? result.FirstOrDefault() : null;

            connection.Close();
            return serialResult;
        }
    }
}
