// Filename: LeagueOfLegendsCache.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using RiotSharp.Endpoints.SummonerEndpoint;

using StackExchange.Redis.Extensions.Core.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Caches
{
    public sealed class LeagueOfLegendsCache : ILeagueOfLegendsCache
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public LeagueOfLegendsCache(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task AddSummonerAsync(UserId userId, Summoner summoner)
        {
            summoner.ProfileIconId = GenerateVerificationProfileIconId(summoner);

            await _redisCacheClient.Db10.AddAsync(GenerateKey(userId), summoner, TimeSpan.FromMinutes(15));
        }

        public async Task RemoveSummonerAsync(UserId userId)
        {
            await _redisCacheClient.Db10.RemoveAsync(GenerateKey(userId));
        }

        public async Task<Summoner> GetSummonerAsync(UserId userId)
        {
            return await _redisCacheClient.Db10.GetAsync<Summoner>(GenerateKey(userId));
        }

        public async Task<bool> SummonerExistsAsync(UserId userId)
        {
            return await _redisCacheClient.Db10.ExistsAsync(GenerateKey(userId));
        }

        private static int GenerateVerificationProfileIconId(Summoner summoner)
        {
            const int ProfileIconIdMinIndex = 0;
            const int ProfileIconIdMaxIndex = 28;

            var random = new Random();

            var profileIconId = summoner.ProfileIconId;

            while (profileIconId == summoner.ProfileIconId)
            {
                profileIconId = random.Next(ProfileIconIdMinIndex, ProfileIconIdMaxIndex);
            }

            return profileIconId;
        }

        private static string GenerateKey(UserId userId)
        {
            return $"{userId}:{nameof(Summoner)}".ToLowerInvariant();
        }
    }
}
