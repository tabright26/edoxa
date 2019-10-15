// Filename: InvitationService.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services.Abstractions;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;

using RiotSharp;
using RiotSharp.Misc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services
{
    public sealed class SummonerService : ISummonerService
    {
        //TODO better to use readonly ????
        private const int LEAGUE_VALID_ICON_STARTING_INDEX = 0;
        private const int LEAGUE_VALID_ICON_ENDING_INDEX = 28;
        private const string LEAGUE_API_KEY = "RGAPI-d164a373-c86f-4fbf-b088-1bc8a4a28054";

        private readonly IDistributedCache _cache;
        private readonly RiotApi _riotApi;
        public SummonerService(IDistributedCache cache)
        {
            _cache = cache;
            _riotApi = RiotApi.GetDevelopmentInstance(LEAGUE_API_KEY);
        }

        public async Task<RiotSharp.Endpoints.SummonerEndpoint.Summoner?> FindSummonerAsync(string summonerName)
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

        public async Task<string> GetSummonerValidationIcon(RiotSharp.Endpoints.SummonerEndpoint.Summoner summoner)
        {
            var requiredSummonerIconId = this.GetDifferentSummonerIconId(summoner.ProfileIconId);

            await _cache.SetAsync(summoner.AccountId, BitConverter.GetBytes(requiredSummonerIconId));

            return requiredSummonerIconId.ToString();
        }

        public async Task<ValidationResult> ValidateSummonerAsync(RiotSharp.Endpoints.SummonerEndpoint.Summoner summoner)
        {
            var cacheIconId = (byte[]?) await _cache.GetAsync(summoner.AccountId);
            await _cache.RemoveAsync(summoner.AccountId);

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


        private int GetDifferentSummonerIconId(int iconId)
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
