using DiscordRPGBot.BusinessLogic.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        public AttributeModifiers AttributeModifiers { get; set; }

        public Class Class { get; set; }

        public Race Race { get; set; }

        public List<Item> Inventory { get; set; }

        public string ProfileImageUrl { get; set; }

        public int Age { get; set; }

        public int HitPointMaximum { get; set; }

        public int CurrentHitPoints { get; set; }

        public string Backstory { get; set; }

        [BsonDateTimeOptions]
        [JsonIgnore]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
