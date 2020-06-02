using LiteDB;

namespace Aliuna.Model.Documents
{
    [CollectionName("offers")]
    public class Offer : Document
    {
        [BsonField("bindoffer")]
        public bool BindOffer { get; set; }
    }
}
