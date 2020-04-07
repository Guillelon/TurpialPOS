using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TurpialPOSDbContext: DbContext
    {
        public DbSet<Store> Store { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Payment> Payment { get; set; }        
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<RegisterCashExit> RegisterCashExit { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Proceeding> Proceeding { get; set; }
        public DbSet<Asset> Asset { get; set; }
    }
}
