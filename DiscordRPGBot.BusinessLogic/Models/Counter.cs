using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordRPGBot.BusinessLogic.Models
{
    public class Counter
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId InternalId { get; set; }

        public string DocumentName { get; set; }

        public long SequenceValue { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; }
    }
}
