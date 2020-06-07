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
    public class ArrivalService : IArrivalService
    {
        private readonly DatabaseFacade _database;

        public ArrivalService(DBContext context)
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

        public async Task<int> GetTotalArrivals(ArrivalFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT COUNT(*)
                            FROM Arrivals A 
                            WHERE " + filterStr;

            var result = connection.Query<int>(sql).FirstOrDefault();
            connection.Close();
            return result;
        }

        public async Task<List<Arrivals>> GetArrivals(ArrivalFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT A.Id AS Id, 
                            A.FID AS FID, 
                            A.ArrivalDate AS ArrivalDate,
                            A.Comments AS Comments,
                            F.Country AS Country, F.City AS City, F.Address AS Address, F.ZipCode AS ZipCode
                            FROM Arrivals A
                            INNER JOIN Facilities F
                                ON F.Id = A.FID
                            WHERE " + filterStr;

            var result = connection.Query<Arrivals>(sql).ToList();

            foreach(var item in result)
            {
                string sql2 = @"SELECT ASe.Serial AS Serial, ASe.Id AS Id
                                FROM ArrivalSerials ASe
                                WHERE ASe.ArrivalID = " + item.Id;
                var serials = connection.Query<Serials>(sql2).ToList();
                item.Serials = serials;
            }
            
            connection.Close();
            return result;
        }

        private string GetFilter(ArrivalFilter filter)
        {
            string filterStr =  "1=1";
            string dateConverted;

            if (filter.ArrivalDateFrom != null)
            {
                dateConverted = filter.ArrivalDateFrom?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND A.ArrivalDate >= '{dateConverted}'";
            }

            if (filter.ArrivalDateTo != null)
            {
                dateConverted = filter.ArrivalDateTo?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND A.ArrivalDate <= '{dateConverted}'";
            }

            if (filter.Id != 0)
            {
                filterStr += $" AND A.Id = {filter.Id}";
            }

            if (!string.IsNullOrEmpty(filter.FID))
            {
                filterStr += $" AND A.FID like '{filter.FID}%'";
            }

            return filterStr;
        }
    }
}
