﻿using LiteDB;

namespace Aliuna.Model
{
    [CollectionName("employees")]
    public class Employee : BaseModel<Employee>
    {
        [BsonField("firstname")]
        public string FirstName { get; set; }

        [BsonField("lastname")]
        public string LastName { get; set; }

        [BsonField("department")]
        public string Department { get; set; } //Indexfeld

        [BsonField("acronym")]
        public string Acronym { get; set; }

        public override string ToString() => Acronym;
    }
}
