// Filename: AuthenticationRepository.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

using StackExchange.Redis.Extensions.Core.Abstractions;

namespace eDoxa.Games.Infrastructure.Repositories
{
    public sealed class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public AuthenticationRepository(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task AddAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game, GameAuthentication<TAuthenticationFactor> gameAuthentication)
        where TAuthenticationFactor : class, IGameAuthenticationFactor
        {
            await _redisCacheClient.Db5.AddAsync(GenerateKey(userId, game), gameAuthentication, TimeSpan.FromMinutes(15));
        }

        public async Task RemoveAuthenticationAsync(UserId userId, Game game)
        {
            await _redisCacheClient.Db5.RemoveAsync(GenerateKey(userId, game));
        }

        public async Task<GameAuthentication<TAuthenticationFactor>> GetAuthenticationAsync<TAuthenticationFactor>(UserId userId, Game game)
        where TAuthenticationFactor : class, IGameAuthenticationFactor
        {
            return await _redisCacheClient.Db5.GetAsync<GameAuthentication<TAuthenticationFactor>>(GenerateKey(userId, game));
        }

        public async Task<GameAuthentication> GetAuthenticationAsync(UserId userId, Game game)
        {
            return await _redisCacheClient.Db5.GetAsync<GameAuthentication>(GenerateKey(userId, game));
        }

        public async Task<bool> AuthenticationExistsAsync(UserId userId, Game game)
        {
            return await _redisCacheClient.Db5.ExistsAsync(GenerateKey(userId, game));
        }

        private static string GenerateKey(UserId userId, Game game)
        {
            return $"{userId}:{game}".ToLowerInvariant();
        }
    }
}
