﻿using DiscordRPGBot.BusinessLogic.Contexts;
using DiscordRPGBot.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordRPGBot.BusinessLogic.Repositories
{
    public class DiscordRPGBotRepository : IDiscordRPGBotRepository
    {
        private readonly DiscordRPGBotContext _context = null;
        private static readonly long _newSequenceValue = 0;

        public DiscordRPGBotRepository(IOptions<Settings> settings)
        {
            _context = new DiscordRPGBotContext(settings);
        }


        //PUBLIC METHODS
        public async Task<IEnumerable<PlayerCharacter>> GetAllPlayerCharacters()
        {
            try
            {
                return await _context.PlayerCharacters.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<PlayerCharacter> GetPlayerCharacter(long id)
        {
            try
            {
                return await _context.PlayerCharacters
                                .Find(PlayerCharacter => PlayerCharacter.Id == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<long> AddPlayerCharacter(PlayerCharacter item)
        {
            try
            {
                item.Id = GetNextSequenceValue("PlayerCharacter");
                await _context.PlayerCharacters.InsertOneAsync(item);
                return item.Id;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemovePlayerCharacter(long id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.PlayerCharacters.DeleteOneAsync(
                        Builders<PlayerCharacter>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdatePlayerCharacter(long id, string characterName)
        {
            var filter = Builders<PlayerCharacter>.Filter.Eq(s => s.Id, id);
            var update = Builders<PlayerCharacter>.Update
                            .Set(s => s.CharacterName, characterName)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult
                    = await _context.PlayerCharacters.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdatePlayerCharacter(long id, PlayerCharacter item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.PlayerCharacters
                                    .ReplaceOneAsync(n => n.Id.Equals(id), item, new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdatePlayerCharacterDocument(long id, string characterName)
        {
            var item = await GetPlayerCharacter(id) ?? new PlayerCharacter();
            item.CharacterName = characterName;
            item.UpdatedOn = DateTime.Now;

            return await UpdatePlayerCharacter(id, item);
        }

        public long AddCounter(string documentName)
        {
            _context.Counters.InsertOne(new Counter()
            {
                DocumentName = documentName,
                SequenceValue = _newSequenceValue,
                UpdatedOn = DateTime.Now
            });

            return _newSequenceValue;
        }


        //PRIVATE METHODS

        private long GetNextSequenceValue(string documentName)
        {
            long currentSequenceValue;
            var currentSequenceList = _context.Counters.Find(c => c.DocumentName.Equals(documentName));

            var sequenceCount = currentSequenceList.CountDocuments();

            if (sequenceCount == 0)
            {
                currentSequenceValue = AddCounter(documentName);
            }
            else
            {
                currentSequenceValue = currentSequenceList.First().SequenceValue;
            }

            var filter = Builders<Counter>.Filter
                            .Eq(c => c.DocumentName, documentName);
            var update = Builders<Counter>.Update
                            .Set(c => c.SequenceValue, (currentSequenceValue + 1))
                            .CurrentDate(s => s.UpdatedOn);

            var sequenceDocument = _context.Counters.FindOneAndUpdate(filter, update);

            return sequenceDocument.SequenceValue;
        }
    }
}
