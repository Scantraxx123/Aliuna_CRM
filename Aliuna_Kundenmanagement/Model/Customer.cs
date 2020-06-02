using LiteDB;
using System.Collections.Generic;

namespace Aliuna.Model
{
    [CollectionName("customers")]
    public class Customer : BaseModel<Customer>
    {
        [BsonField("companyname")]
        public string CompanyName { get; set; }

        [BsonField("firstname")]
        public string FirstName { get; set; }

        [BsonField("lastname")]
        public string LastName { get; set; }

        [BsonField("email")]
        public string Email { get; set; }

        [BsonField("street")]
        public string Street { get; set; }

        [BsonField("housenumber")]
        public string Housenumber { get; set; }

        [BsonField("postcode")]
        public string Postcode { get; set; }

        [BsonField("city")]
        public string City { get; set; }

        [BsonField("country")]
        public string Country { get; set; }

        [BsonField("phonemumber")]
        public string PhoneNumber { get; set; }

        [BsonField("faxnumber")]
        public string FaxNumber { get; set; }

        //[BsonRef("orders")]
        [BsonField("orders")]
        public List<Order> Orders { get; set; }

    }
}
