using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Infrastructure
{
    public class DBContext : DbContext
    {
        public DbSet<Arrivals> Arrivals { get; set; }
        public DbSet<ArrivalSerials> ArrivalSerials { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Dispatches> Dispatches { get; set; }
        public DbSet<DispatchSerials> DispatchSerials { get; set; }
        public DbSet<EconomicOperators> EconomicOperators { get; set; }
        public DbSet<Facilities> Facilities { get; set; }
        public DbSet<InvoicesDB> Invoices { get; set; }
        public DbSet<InvoiceSerials> InvoiceSerials { get; set; }
        public DbSet<Machines> Machines { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<RequestSerials> RequestSerials { get; set; }
        public DbSet<SerialsDB> Serials { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
         : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            optionsBuilder.UseSqlServer("Server=tfgalex.database.windows.net,1433;" +
                "Initial Catalog=TFGAlex;" +
                "Persist Security Info=False;" +
                "User ID=alex;" +
                "Password=Tefege1234." +
                ";MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;");
        }

    }
}
