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
    public class InvoiceService : IInvoiceService
    {
        private readonly DatabaseFacade _database;

        public InvoiceService(DBContext context)
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

        public async Task<int> GetTotalInvoices(InvoiceFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT COUNT(*)
                            FROM Invoices I 
                            WHERE " + filterStr;

            var result = connection.Query<int>(sql).FirstOrDefault();
            connection.Close();
            return result;
        }

        public async Task<List<Invoices>> GetInvoices(InvoiceFilter filter)
        {
            string filterStr = GetFilter(filter);

            DbConnection connection = GetConnection();

            string sql = @" SELECT I.Id AS Id,  
                            I.InvoiceDate AS InvoiceDate,
                            I.Price AS Price,
                            I.Currency AS Currency,
                            I.BuyerEU AS BuyerEU,
                            I.BuyerID AS BuyerID, 
                            F.Country AS BuyerCountry, F.Name AS BuyerName, F.City AS BuyerCity, F.Address AS BuyerAddress, F.ZipCode AS BuyerZipCode
                            FROM Invoices I
                            INNER JOIN Facilities F
                                ON F.Id = I.BuyerID
                            WHERE " + filterStr;

            var result = connection.Query<Invoices>(sql).ToList();

            foreach(var item in result)
            {
                string sql2 = @" SELECT ISE.Serial AS Serial, ISE.Id AS Id
                            FROM InvoiceSerials ISE
                            WHERE ISE.InvoiceID = " + item.Id;
                var serials = connection.Query<Serials>(sql2).ToList();
                item.Serials = serials;
            }
            
            connection.Close();
            return result;
        }

        private string GetFilter(InvoiceFilter filter)
        {
            string filterStr =  "1=1";
            string dateConverted;

            if (filter.InvoiceDateFrom != null)
            {
                dateConverted = filter.InvoiceDateFrom?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND I.InvoiceDate >= '{dateConverted}'";
            }

            if (filter.InvoiceDateTo != null)
            {
                dateConverted = filter.InvoiceDateTo?.ToString("yyyy-MM-ddTHH:mm:ssZ");
                filterStr += $" AND I.InvoiceDate <= '{dateConverted}'";
            }

            if (filter.Id != 0)
            {
                filterStr += $" AND I.Id = {filter.Id}";
            }

            if (!string.IsNullOrEmpty(filter.BuyerID))
            {
                filterStr += $" AND I.BuyerID like '{filter.BuyerID}%'";
            }

            if(filter.BuyerEU == true)
            {
                filterStr += $" AND I.BuyerEU = 1";
            }
            else
            {
                filterStr += $" AND I.BuyerEU = 0";
            }

            if (!string.IsNullOrEmpty(filter.Currency))
            {
                filterStr += $" AND I.Currency like '{filter.Currency}%'";
            }

            if (filter.Price != 0.0)
            {
                filterStr += $" AND I.Price >= {filter.Price}";
            }

            return filterStr;
        }

        public async Task<Invoices> GetInvoiceById(int id)
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT I.Id AS Id 
                            FROM Invoices AS I
                            WHERE I.Id = '" + id + "'";

            var result = connection.Query<Invoices>(sql);

            var invoiceResult = result.Any() ? result.FirstOrDefault() : null;

            connection.Close();
            return invoiceResult;
        }

        public async Task<int> GetLastIdInvoice()
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT MAX(I.Id) AS Id FROM Invoices AS I";

            var result = connection.Query<int>(sql).ToList();

            var lastId = result.Any() ? result.FirstOrDefault() : 0;

            connection.Close();
            return lastId;
        }

        public async Task<int> GetLastIdInvoiceSerials()
        {
            DbConnection connection = GetConnection();

            string sql = @" SELECT MAX(I.Id) AS Id FROM InvoiceSerials AS I";

            var result = connection.Query<int>(sql).ToList();

            var lastId = result.Any() ? result.FirstOrDefault() : 0;

            connection.Close();
            return lastId;
        }

        public async Task<int> UpdateInvoiceSerialsTable(InvoiceSerials item)
        {
            DbConnection connection = GetConnection();

            string insertQuery = @"INSERT INTO [dbo].[InvoiceSerials]([Id], [Serial], [InvoiceID]) VALUES (" + 
                item.Id + ", '" + item.Serial + "', " + item.InvoiceID +")";

            var result = connection.Execute(insertQuery);

            connection.Close();
            return result;
        }
    }
}
