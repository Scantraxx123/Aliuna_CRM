using Aliuna_Kundenmanagement.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

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
            if (db != null) db.Close();

        }
        public static bool isConnectionOpen()
        {
            if (db == null) return false;
            return true;
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

        public static void DeleteCustomer(int toDelete)
        {
            db.Delete<Customer>(toDelete);
        }

        public static void UpdateCustomer(Customer toUpdate)
        {
            db.Update(toUpdate, typeof(Customer));
        }

        public static IEnumerable<Customer> GetCustomers()
        {
            var query = db.Table<Customer>();
            var list = query.ToList();
            return list;
        }
    }
}
