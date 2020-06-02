using LiteDB;
using System;
using System.Collections.Generic;


namespace Aliuna.Model
{
    //Products and Amount need to be seen as one field, but database can not handle it
    public class Document : BaseModel<Document>
    {
        [BsonField("products")]
        //[BsonRef("products")]
        public List<Product> Products { get; set; } = new List<Product>();

        [BsonField("amount")]
        public List<int> Amount { get; set; }
    }
}
