using Aliuna_Kundenmanagement.Model;
using SQLite;
using System.Collections.Generic;

namespace Aliuna_Kundenmanagement.Controller
{
    sealed class DatabaseController
    {
        private volatile static DatabaseController _db = null;
        private SQLiteConnection _sqlCon = null;
        // Hilfsfeld für eine sichere Threadsynchronisierung
        private static object m_lock = new object();

        private DatabaseController()
        {
        }

        public static DatabaseController GetInstance()
        {
            if (_db == null)
                lock (m_lock) { if (_db == null) _db = new DatabaseController(); }
            return _db;
        }
        public bool IsConnectionOpen()
        {
            if (this._sqlCon == null) return false;
            return true;
        }

        public void EstablishConnection(string path, string pw)
        {
            if (this._sqlCon == null && !path.Equals(string.Empty))
            {
                var options = new SQLiteConnectionString(path, true, key: pw);
                this._sqlCon = new SQLiteConnection(options);
            }

        }

        public void CloseConnection()
        {
            if (this._sqlCon != null)
            {
                this._sqlCon.Close();
                this._sqlCon = null;
            }
        }

        public void CreateDatabase(string filePath, string pw)
        {
            CloseConnection();
            var options = new SQLiteConnectionString(filePath, true, key: pw);
            this._sqlCon = new SQLiteConnection(options);
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
