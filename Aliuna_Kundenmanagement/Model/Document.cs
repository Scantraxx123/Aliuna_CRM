using LiteDB;
using System;
using System.Collections.Generic;


namespace Aliuna.Model
{
    //Products and Amount need to be seen as one field, but database can not handle it
    class Document : BaseModel<Document>
    {
        [BsonField("products")]
        //[BsonRef("products")]
        public List<Product> Products { get; set; } = new List<Product>();

        [BsonField("amount")]
        public List<int> Amount { get; set; }

        [BsonField("notes")]
        public List<string> Notes { get; set; } = new List<string>();

        [BsonField("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonField("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
