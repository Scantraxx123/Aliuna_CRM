using Aliuna.Model.Subclasses;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aliuna.Model
{
    public class BaseModel<T> where T : new()
    {
        [BsonId(true)]
        public int Id { get; set; }

        [BsonField("notes")]
        public List<Note> Notes { get; set; } = new List<Note>();

        [BsonField("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonField("updatedat")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public static ConnectionString connectionString = null;

        public static string CollectionName()
        {
            var tableAttr = (CollectionNameAttribute)typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault();
            if (tableAttr != null)
            {
                return tableAttr.Name;
            }

            string name = typeof(T).Name.ToLower();

            if (name.EndsWith("y"))
            {
                name = name.Substring(0, name.Length - 1) + "ies";
            }
            else if (name.EndsWith("ch") || name.EndsWith("x") || name.EndsWith("ss"))
            {
                name = name + "es";
            }
            else if (!name.EndsWith("s"))
            {
                name += "s";
            }

            return name;
        }

        private static List<string> GetIncludes(string collectionName)
        {
            switch (collectionName)
            {
                case "deliveries":
                    return new List<string> {
                        "$.products[*]",
                        "$.relatedinvoice"
                    };
                case "invoices":
                    return new List<string> {
                        "$.products[*]",
                        "$.relatedoffer"
                    };
                case "offers":
                    return new List<string> {
                        "$.products[*]",
                    };
                case "customers":
                    return new List<string>
                    {
                        "$.orders",
                        "$.orders[*].employee",
                    };
                case "orders":
                    return new List<string> {
                        "$.employee",
                        "$.customer",
                        "$.offers[*]",
                        "$.invoice",
                        "$.delivery",
                        "$.products[*]"
                    };
                case "products":
                    return null;
                case "employees":
                    return null;
                default:
                    return null;
            }
        }

        public static void EnsureIndex(Expression<Func<T, object>> predicate)
        {

            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<T>(CollectionName());
                collection.EnsureIndex(predicate);
            }
        }

        public static List<T> GetAll()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var cn = CollectionName();
                var collection = db.GetCollection<T>(cn);
                var list = GetIncludes(cn);
                if (list != null) foreach (var item in list) collection = collection.Include(item);
                List<T> result = collection.FindAll().ToList();
                return result;
            }
        }

        public static List<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var cn = CollectionName();
                var collection = db.GetCollection<T>(cn);
                var list = GetIncludes(cn);
                if (list != null) foreach (var item in list) collection = collection.Include(item);
                List<T> result = collection.Find(predicate).ToList();
                return result;
            }
        }

        public static T GetOne(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var cn = CollectionName();
                var collection = db.GetCollection<T>(cn);
                var list = GetIncludes(cn);
                if (list != null) foreach (var item in list) collection = collection.Include(item);
                T result = collection.Find(predicate).First();
                return result;
            }
        }

        public void Save()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<T>(CollectionName());

                collection.Upsert((T)(object)this);
            }
        }

        public void SaveList(List<T> list)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<T>(CollectionName());

                collection.Upsert(list);
            }
        }

        public void Delete()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var collection = db.GetCollection<T>(CollectionName());

                collection.Delete(this.Id);
            }
        }
    }
}
