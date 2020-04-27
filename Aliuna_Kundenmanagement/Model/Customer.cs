using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliuna_Kundenmanagement.Model
{
    class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string company { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public Customer()
        {

        }
    }
}
