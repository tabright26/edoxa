// Filename: AuthFactorRepository.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

using StackExchange.Redis.Extensions.Core.Abstractions;

namespace eDoxa.Games.Infrastructure.Repositories
{
    public sealed class AuthFactorRepository : IAuthFactorRepository
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public AuthFactorRepository(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task AddAuthFactorAsync(UserId userId, Game game, AuthFactor authFactor)
        {
            await _redisCacheClient.Db5.AddAsync(GenerateKey(userId, game), authFactor, TimeSpan.FromMinutes(15));
        }

        public async Task RemoveAuthFactorAsync(UserId userId, Game game)
        {
            await _redisCacheClient.Db5.RemoveAsync(GenerateKey(userId, game));
        }

        public async Task<AuthFactor> GetAuthFactorAsync(UserId userId, Game game)
        {
            return await _redisCacheClient.Db5.GetAsync<AuthFactor>(GenerateKey(userId, game));
        }

        public async Task<bool> AuthFactorExistsAsync(UserId userId, Game game)
        {
            return await _redisCacheClient.Db5.ExistsAsync(GenerateKey(userId, game));
        }

        private static string GenerateKey(UserId userId, Game game)
        {
            return $"{userId}:{game}".ToLowerInvariant();
        }
    }
}
