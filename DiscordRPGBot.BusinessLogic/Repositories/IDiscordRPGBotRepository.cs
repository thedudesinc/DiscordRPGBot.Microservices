using DiscordRPGBot.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordRPGBot.BusinessLogic.Repositories
{
    public interface IDiscordRPGBotRepository
    {      
        Task<IEnumerable<PlayerCharacter>> GetAllPlayerCharacters();

        Task<PlayerCharacter> GetPlayerCharacter(string discordId);

        Task<long> AddPlayerCharacter(PlayerCharacter item);

        Task<bool> RemovePlayerCharacter(string discordId);

        Task<bool> UpdatePlayerCharacter(string discordId, string characterName);

        Task<bool> UpdatePlayerCharacterDocument(string discordId, string characterName);
    }
}
