using LiteDB;

namespace Aliuna.Model.Documents
{
    [CollectionName("deliveries")]
    class Delivery : Document 
    {
        [BsonField("relatedinvoice")]
        //[BsonRef("invoices")]
        public Invoice RelatedInvoice { get; set; }
    }
}
