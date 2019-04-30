using DiscordRPGBot.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DiscordRPGBot.BusinessLogic.Contexts
{
    public class DiscordRPGBotContext
    {
        private readonly IMongoDatabase _database = null;

        public DiscordRPGBotContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<PlayerCharacter> PlayerCharacters
        {
            get
            {
                return _database.GetCollection<PlayerCharacter>("PlayerCharacter");
            }
        }

        public IMongoCollection<Counter> Counters
        {
            get
            {
                return _database.GetCollection<Counter>("Counter");
            }
        }
    }
}
