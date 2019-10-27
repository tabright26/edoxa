// Filename: LeagueOfLegendsSummonerService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation.Results;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using StackExchange.Redis.Extensions.Core.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Services
{
    public sealed class LeagueOfLegendsSummonerService : ILeagueOfLegendsSummonerService
    {
        //TODO better to use readonly ????
        private const int LEAGUE_VALID_ICON_STARTING_INDEX = 0;
        private const int LEAGUE_VALID_ICON_ENDING_INDEX = 28;
        private const string LEAGUE_API_KEY = "RGAPI-d164a373-c86f-4fbf-b088-1bc8a4a28054";

        private readonly IRedisCacheClient _redisCacheClient;
        private readonly RiotApi _riotApi;

        public LeagueOfLegendsSummonerService(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
            _riotApi = RiotApi.GetDevelopmentInstance(LEAGUE_API_KEY);
        }

        public async Task<Summoner?> FindSummonerAsync(string summonerName)
        {
            try
            {
                //If the name does not exist the league API call has an internal server error. Remove when we have the public key that doesnt expire.
                var summoner = await _riotApi.Summoner.GetSummonerByNameAsync(Region.Na, summonerName);

                return summoner;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetSummonerValidationIcon(Summoner summoner)
        {
            var requiredSummonerIconId = GetDifferentSummonerIconId(summoner.ProfileIconId);

            await _redisCacheClient.Db10.Database.StringSetAsync(summoner.AccountId, requiredSummonerIconId);

            return requiredSummonerIconId.ToString();
        }

        public async Task<ValidationResult> ValidateSummonerAsync(Summoner summoner)
        {
            var cacheIconId = (byte[]?) await _redisCacheClient.Db10.Database.StringGetAsync(summoner.AccountId);

            await _redisCacheClient.Db10.RemoveAsync(summoner.AccountId);

            if (cacheIconId == null)
            {
                return new ValidationFailure(string.Empty, "Validation expired or never existed.").ToResult();
            }

            if (BitConverter.ToInt32(cacheIconId) != summoner.ProfileIconId)
            {
                return new ValidationFailure(string.Empty, "Icon does not match, validation failed.").ToResult();
            }

            return new ValidationResult();
        }

        private static int GetDifferentSummonerIconId(int iconId)
        {
            var randomizer = new Random();
            var requiredIcon = iconId;

            while (requiredIcon == iconId)
            {
                requiredIcon = randomizer.Next(LEAGUE_VALID_ICON_STARTING_INDEX, LEAGUE_VALID_ICON_ENDING_INDEX);
            }

            return requiredIcon;
        }
    }
}
