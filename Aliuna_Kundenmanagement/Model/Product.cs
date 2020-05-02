using LiteDB;
using System;
using System.Collections.Generic;

namespace Aliuna.Model
{
    [CollectionName("products")]
    class Product : BaseModel<Product>
    {

        [BsonField("name")]
        public string Name { get; set; }

        [BsonField("manufacturer")]
        public string Manufacturer { get; set; } //Indexfeld

        [BsonField("price")]
        public double price { get; set; }

        [BsonField("instock")]
        public int InStock { get; set; }

        [BsonField("reserved")]
        public int Reserved { get; set; }

        [BsonField("sold")]
        public int Sold { get; set; }

        [BsonField("images")]
        public List<string> Images { get; set; } = new List<string>(); //Base 64 Strings maybe?

        [BsonField("notes")]
        public List<string> Notes { get; set; } = new List<string>();

        [BsonField("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonField("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
