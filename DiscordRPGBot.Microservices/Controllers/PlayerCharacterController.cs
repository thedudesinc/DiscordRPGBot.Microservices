using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiscordRPGBot.BusinessLogic.Models;
using DiscordRPGBot.BusinessLogic.Repositories;
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
        [HttpGet("{id}")]
        public async Task<PlayerCharacter> Get(long id)
        {
            return await _repository.GetPlayerCharacter(id) ?? new PlayerCharacter();
        }

        // POST api/playercharacter
        [HttpPost]
        public async Task<long> Post([FromBody] PlayerCharacter pc)
        {
            var id = await _repository.AddPlayerCharacter(pc);
            return id;
        }

        // PUT api/playercharacter/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody] string characterName)
        {
            _repository.UpdatePlayerCharacterDocument(id, characterName);
        }

        // DELETE api/playercharacter/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _repository.RemovePlayerCharacter(id);
        }
    }
}