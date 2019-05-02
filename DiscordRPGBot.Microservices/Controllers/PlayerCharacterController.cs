using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiscordRPGBot.BusinessLogic.Models;
using DiscordRPGBot.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRPGBot.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCharacterController : ControllerBase
    {
        private readonly IDiscordRPGBotRepository _repository;

        public PlayerCharacterController(IDiscordRPGBotRepository repository)
        {
            _repository = repository;
        }

        // GET api/playercharacter
        [HttpGet]
        public async Task<IEnumerable<PlayerCharacter>> Get()
        {
            return await _repository.GetAllPlayerCharacters();
        }

        // GET api/playercharacter/5
        [HttpGet("{discordId}")]
        public async Task<PlayerCharacter> Get(string discordId)
        {
            return await _repository.GetPlayerCharacter(discordId) ?? new PlayerCharacter();
        }

        // POST api/playercharacter
        [HttpPost]
        public async Task<long> Post([FromBody] PlayerCharacter pc)
        {
            var id = await _repository.AddPlayerCharacter(pc);
            return id;
        }

        // PUT api/playercharacter/5
        [HttpPut("{discordId}")]
        public void Put(string discordId, [FromBody] string characterName)
        {
            _repository.UpdatePlayerCharacterDocument(discordId, characterName);
        }

        // DELETE api/playercharacter/5
        [HttpDelete("{discordId}")]
        public void Delete(string discordId)
        {
            _repository.RemovePlayerCharacter(discordId);
        }
    }
}