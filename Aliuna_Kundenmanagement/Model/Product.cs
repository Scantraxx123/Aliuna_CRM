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
        public double Price { get; set; }

        [BsonField("instock")]
        public int InStock { get; set; }

        [BsonField("reserved")]
        public int Reserved { get; set; }

        [BsonField("sold")]
        public int Sold { get; set; }

        [BsonField("images")]
        public List<string> Images { get; set; } = new List<string>(); //Base 64 Strings maybe?
    }
}
