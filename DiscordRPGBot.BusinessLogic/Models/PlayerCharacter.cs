using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace DiscordRPGBot.BusinessLogic.Models
{
    public class PlayerCharacter
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId InternalId { get; set; }

        public long Id { get; set; }

        public string DiscordId { get; set; }

        public string CharacterName { get; set; }

        [BsonDateTimeOptions]
        [JsonIgnore]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
