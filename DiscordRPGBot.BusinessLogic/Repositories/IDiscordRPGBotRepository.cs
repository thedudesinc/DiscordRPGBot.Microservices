using DiscordRPGBot.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordRPGBot.BusinessLogic.Repositories
{
    public interface IDiscordRPGBotRepository
    {      
        Task<IEnumerable<PlayerCharacter>> GetAllPlayerCharacters();

        Task<PlayerCharacter> GetPlayerCharacter(long id);

        Task<long> AddPlayerCharacter(PlayerCharacter item);

        Task<bool> RemovePlayerCharacter(long id);

        Task<bool> UpdatePlayerCharacter(long id, string characterName);

        Task<bool> UpdatePlayerCharacterDocument(long id, string characterName);
    }
}
