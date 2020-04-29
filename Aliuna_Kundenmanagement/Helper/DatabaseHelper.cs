using Aliuna_Kundenmanagement.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aliuna_Kundenmanagement.Helper
{
    sealed class DatabaseHelper
    {
        private volatile static DatabaseHelper _db = null;
        private SQLiteConnection _sqlCon = null;
        // Hilfsfeld für eine sichere Threadsynchronisierung
        private static object m_lock = new object();

        private DatabaseHelper()
        {
        }

        public static DatabaseHelper GetInstance()
        {
            if (_db == null)
                lock (m_lock) { if (_db == null) _db = new DatabaseHelper(); }
            return _db;
        }
        public bool IsConnectionOpen()
        {
            if (this._sqlCon == null) return false;
            return true;
        }

        public void EstablishConnection(string path)
        {
            if (this._sqlCon == null && !path.Equals(string.Empty)) this._sqlCon = new SQLiteConnection(path);
        }

        public void CloseConnection()
        {
            if (this._sqlCon != null)
            {
                this._sqlCon.Close();
                this._sqlCon = null;
            }
        }

        public void CreateDatabase(string filePath)
        {
            CloseConnection();
            var tempDatabasePath = Path.Combine(Environment.CurrentDirectory, @"Data\tempDatabase.db");
            File.Copy(tempDatabasePath, filePath, true);
            var newDatabasePath = Path.Combine(filePath);
            this._sqlCon = new SQLiteConnection(newDatabasePath);
            this._sqlCon.CreateTable<Customer>();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            var query = this._sqlCon.Table<Customer>();
            var list = query.ToList();
            return list;
        }
        public void AddCustomer(Customer toSave)
        {
            this._sqlCon.Insert(toSave, typeof(Customer));
        }

        public void AddCustomer(IEnumerable<Customer> toSave)
        {
            this._sqlCon.InsertAll(toSave, typeof(Customer));
        }

        public void DeleteCustomer(int toDelete)
        {
            this._sqlCon.Delete<Customer>(toDelete);
        }

        public void UpdateCustomer(Customer toUpdate)
        {
            this._sqlCon.Update(toUpdate, typeof(Customer));
        }


    }
}
