using Aliuna.Model;
using Aliuna.Model.Documents;
using LiteDB;

namespace Aliuna.Controller
{
    class DatabaseController
    {
        /// <summary>
        /// Create or establish connection with database with all References, Indexes 
        /// </summary>
        /// <param name="fileName">Path to database</param>
        /// <param name="pw">password for databae</param>
        /// <returns></returns>
        public static void SetDatabase(string fileName, string pw)
        {
            //https://www.litedb.org/docs/connection-string/
            //var cn = new ConnectionString($"filename={fileName};connection=shared;password={result};upgrade=true");
            SetConnectionStrings(new ConnectionString($"filename={fileName};password={pw};upgrade=true"));
            SetIndexes();
            SetReferences();
            App.isDBConOpen = true;
        }

        private static void SetReferences()
        {
            BsonMapper.Global.Entity<Delivery>().DbRef(x => x.RelatedInvoice, "invoices");
            BsonMapper.Global.Entity<Delivery>().DbRef(x => x.Products, "products");
            BsonMapper.Global.Entity<Invoice>().DbRef(x => x.RelatedOffer, "offers");
            BsonMapper.Global.Entity<Invoice>().DbRef(x => x.Products, "products");
            BsonMapper.Global.Entity<Offer>().DbRef(x => x.Products, "products");
            BsonMapper.Global.Entity<Customer>().DbRef(x => x.Orders, "orders");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Employee, "employees");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Customer, "customers");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Offers, "offers");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Invoice, "invoices");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Delivery, "deliveries");
            BsonMapper.Global.Entity<Order>().DbRef(x => x.Products, "products");
        }

        private static void SetIndexes()
        {
            BaseModel<Employee>.EnsureIndex(x => x.Department);
            BaseModel<Order>.EnsureIndex(x => x.Employee);
            BaseModel<Order>.EnsureIndex(x => x.Customer);
            BaseModel<Product>.EnsureIndex(x => x.Manufacturer);
        }

        private static void SetConnectionStrings(ConnectionString cs)
        {
            BaseModel<Document>.connectionString = cs;
            BaseModel<Customer>.connectionString = cs;
            BaseModel<Delivery>.connectionString = cs;
            BaseModel<Employee>.connectionString = cs;
            BaseModel<Invoice>.connectionString = cs;
            BaseModel<Offer>.connectionString = cs;
            BaseModel<Order>.connectionString = cs;
            BaseModel<Product>.connectionString = cs;
        }

    }
}
