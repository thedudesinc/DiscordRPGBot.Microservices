using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordRPGBot.Microservices.ViewModels
{
    public class PlayerCharacterVM
    {
        public int DiscordId { get; set; }
        public string CharacterName { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
    }
}
