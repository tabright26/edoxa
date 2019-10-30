// Filename: SummonerService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

using StackExchange.Redis.Extensions.Core.Abstractions;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Services
{
    public sealed class LeagueOfLegendsSummonerService : ILeagueOfLegendsSummonerService
    {
        private const string ApiKey = "RGAPI-d164a373-c86f-4fbf-b088-1bc8a4a28054";

        private static readonly RiotApi RiotApi;

        private readonly ILeagueOfLegendsCredentialService _credentialService;
        private readonly IRedisCacheClient _redisCacheClient;

        static LeagueOfLegendsSummonerService()
        {
            RiotApi = RiotApi.GetDevelopmentInstance(ApiKey);
        }

        public LeagueOfLegendsSummonerService(ILeagueOfLegendsCredentialService credentialService, IRedisCacheClient redisCacheClient)
        {
            _credentialService = credentialService;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<Summoner?> FindSummonerAsync(string summonerName)
        {
            try
            {
                //If the name does not exist the league API call has an internal server error. Remove when we have the public key that doesnt expire.
                return await RiotApi.Summoner.GetSummonerByNameAsync(Region.Na, summonerName);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Summoner?> FindSummonerAsync(UserId userId)
        {
            await _redisCacheClient.Db10.AddAsync(nameof(PlayerId), "Test");

            return new Summoner();
        }

        public async Task<int> GenerateDifferentProfileIconIdAsync(Summoner summoner)
        {
            var summonerProfileIconId = _credentialService.GenerateDifferentProfileIconId(summoner.ProfileIconId);

            await _redisCacheClient.Db10.AddAsync(summoner.AccountId, summonerProfileIconId);

            return summonerProfileIconId;
        }

        public async Task<ValidationResult> ValidateSummonerAsync(Summoner summoner)
        {
            var summonerProfileIconId = await _redisCacheClient.Db10.GetAsync<int?>(summoner.AccountId);

            await _redisCacheClient.Db10.RemoveAsync(summoner.AccountId);

            if (summonerProfileIconId == null)
            {
                return new ValidationFailure(string.Empty, "Validation expired or never existed.").ToResult();
            }

            if (summonerProfileIconId != summoner.ProfileIconId)
            {
                return new ValidationFailure(string.Empty, "Icon does not match, validation failed.").ToResult();
            }

            return new ValidationResult();
        }
    }
}
