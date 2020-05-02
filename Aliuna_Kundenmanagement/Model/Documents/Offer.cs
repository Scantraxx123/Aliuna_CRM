using LiteDB;

namespace Aliuna.Model.Documents
{
    [CollectionName("offers")]
    class Offer : Document
    {
        [BsonField("bindoffer")]
        public bool BindOffer { get; set; }
    }
}
