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
    public class FacilityService : IFacilityService
    {
        private readonly DatabaseFacade _database;

        public FacilityService(DBContext context)
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

        public async Task<Facilities> GetFacilityById(string facilityId)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT *
                            FROM Facilities F
                            WHERE F.Id = '" + facilityId + "'";

            var result = connection.Query<Facilities>(sql).FirstOrDefault();

            connection.Close();
            return result;
        }

        public async Task<Facilities> GetFacilityByFacility(string facilityName, string country, string city, string address, string zipcode)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT *
                            FROM Facilities F
                            WHERE LOWER(F.Name) = '" + facilityName.ToLower() + "'" +
                            " AND LOWER(F.Country) = '" + country.ToLower() + "'" +
                            " AND LOWER(F.City) = '" + city.ToLower() + "'" +
                            " AND LOWER(F.Address) = '" + address.ToLower() + "'" +
                            " AND LOWER(F.ZipCode) = '" + zipcode.ToLower() + "'";

            var result = connection.Query<Facilities>(sql).FirstOrDefault();

            connection.Close();
            return result;
        }
    }
}
