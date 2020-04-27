using Aliuna_Kundenmanagement.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;


namespace Aliuna_Kundenmanagement.Helper
{
    class DatabaseHelper
    {
        private static SQLiteConnection db = null;

        public static void EstablishConnection(string path)
        {
            db = new SQLiteConnection(path);
            
        }
        public static void CloseConnection()
        {
            if(db != null) db.Close();

        }
        public static void CreateDatabase(string filePath)
        {
            var tempDatabasePath = Path.Combine(Environment.CurrentDirectory, @"Data\tempDatabase.db");
            File.Copy(tempDatabasePath, filePath, true);
            var newDatabasePath = Path.Combine(filePath);
            db = new SQLiteConnection(newDatabasePath);
            db.CreateTable<Customer>();
        }
        public static void AddCustomer(Customer toSave)
        {
            db.Insert(toSave, typeof(Customer));

        }

        public static void AddCustomer(IEnumerable<Customer> toSave)
        {
            db.InsertAll(toSave, typeof(Customer));
        }
        public static IEnumerable<Customer> GetCustomers()
        {
            var query = db.Table<Customer>();
            var list = query.ToList();
            return list;
        }
    }
}
