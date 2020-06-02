using Aliuna.Model.Documents;
using LiteDB;
using System.Collections.Generic;

namespace Aliuna.Model
{
    [CollectionName("orders")]
    public class Order : BaseModel<Order>
    {
        [BsonField("employee")]
        //[BsonRef("employees")]
        public Employee Employee { get; set; } //Indexfeld

        [BsonField("customer")]
        //[BsonRef("customers")]
        public Customer Customer { get; set; } //Indexfeld

        [BsonField("offers")]
        //[BsonRef("offers")]
        public List<Offer> Offers { get; set; }

        [BsonField("invoice")]
        //[BsonRef("invoices")]
        public Invoice Invoice { get; set; }

        [BsonField("delivery")]
        //[BsonRef("deliveries")]
        public Delivery Delivery { get; set; }

        [BsonField("products")]
        //[BsonRef("products")]
        public List<Product> Products { get; set; }

    }
}
