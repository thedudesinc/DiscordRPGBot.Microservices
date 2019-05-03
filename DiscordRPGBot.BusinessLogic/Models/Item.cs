using DiscordRPGBot.BusinessLogic.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordRPGBot.BusinessLogic.Models
{
    public class Item
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId InternalId { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ItemType Type { get; set; }

        public ItemSubType SubType { get; set; }

        public Rarity Rarity { get; set; }

        [BsonDateTimeOptions]
        [JsonIgnore]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
