using SQLite;

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
        public string postcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public Customer()
        {

        }

        public Customer(string company, string firstName, string lastName, string email, string street, string housenumber, string postcode, string city, string country)
        {
            this.company = company;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.street = street;
            this.housenumber = housenumber;
            this.postcode = postcode;
            this.city = city;
            this.country = country;
        }
    }
}
