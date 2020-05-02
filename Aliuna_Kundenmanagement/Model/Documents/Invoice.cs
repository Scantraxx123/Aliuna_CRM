using LiteDB;

namespace Aliuna.Model.Documents
{
    [CollectionName("invoices")]
    class Invoice : Document
    {
        [BsonField("relatedoffer")]
        //[BsonRef("offers")]
        public Offer RelatedOffer { get; set; }

        [BsonField("ispaid")]
        public bool IsPaid { get; set; }
    }
}
