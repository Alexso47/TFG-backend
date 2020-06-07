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
    public class DispatchService : IDispatchService
    {
        private readonly DatabaseFacade _database;

        public DispatchService(DBContext context)
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

        public async Task<int> GetTotalDispatches(DispatchFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT COUNT(*)
                            FROM Dispatches D 
                            WHERE " + filterStr;

            var result = connection.Query<int>(sql).FirstOrDefault();
            connection.Close();
            return result;
        }

        public async Task<List<Dispatches>> GetDispatches(DispatchFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT D.Id AS Id, 
                            D.FID AS FID, 
                            D.DispatchDate AS DispatchDate,
                            D.DestinationEU AS DestinationEU,
                            D.DestinationFID AS DestinationFID,
                            D.TransportMode AS TransportMode,
                            D.Vehicle AS Vehicle, 
                            F.Country AS DestinationCountry, F.City AS DestinationCity, F.Address AS DestinationAddress, F.ZipCode AS DestinationZipCode
                            FROM Dispatches D
                            INNER JOIN Facilities F
                                ON F.Id = D.DestinationFID
                            WHERE " + filterStr;

            var result = connection.Query<Dispatches>(sql).ToList();

            foreach(var item in result)
            {
                string sql2 = @" SELECT DS.Serial AS Serial, DS.Id AS Id
                            FROM DispatchSerials DS
                            WHERE DS.DispatchID = " + item.Id;
                var serials = connection.Query<Serials>(sql2).ToList();
                item.Serials = serials;
            }
            
            connection.Close();
            return result;
        }

        private string GetFilter(DispatchFilter filter)
        {
            string filterStr =  "1=1";
            string dateConverted;

            if (filter.DispatchDateFrom != null)
            {
                dateConverted = filter.DispatchDateFrom?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND D.DispatchDate >= '{dateConverted}'";
            }

            if (filter.DispatchDateTo != null)
            {
                dateConverted = filter.DispatchDateTo?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND D.DispatchDate <= '{dateConverted}'";
            }

            if (filter.Id != 0)
            {
                filterStr += $" AND D.Id = {filter.Id}";
            }

            if (!string.IsNullOrEmpty(filter.FID))
            {
                filterStr += $" AND D.FID like '{filter.FID}%'";
            }

            if (filter.DestinationEU != null)
            {
                if(filter.DestinationEU == true)
                {
                    filterStr += $" AND D.DestinationEU = 1";
                }
                else
                {
                    filterStr += $" AND D.DestinationEU = 0";
                }
                
            }

            if (!string.IsNullOrEmpty(filter.DestinationFID))
            { 
                filterStr += $" AND D.DestinationFID like '{filter.DestinationFID}%'";
            }

            if (!string.IsNullOrEmpty(filter.Vehicle))
            {
                filterStr += $" AND D.Vehicle like '{filter.Vehicle}%'";
            }

            if (!string.IsNullOrEmpty(filter.TransportMode))
            {
                filterStr += $" AND D.TransportMode like '{filter.TransportMode}%'";
            }

            return filterStr;
        }
    }
}
