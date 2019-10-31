// Filename: LeagueOfLegendsSummonerService.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Caches;
using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services
{
    public sealed class LeagueOfLegendsSummonerService : ILeagueOfLegendsSummonerService
    {
        private const string ApiKey = "RGAPI-6785ca83-ff29-4035-aa34-525eebea1105";

        private static readonly RiotApi RiotApi;

        private readonly ILeagueOfLegendsCache _leagueOfLegendsCache;

        static LeagueOfLegendsSummonerService()
        {
            RiotApi = RiotApi.GetDevelopmentInstance(ApiKey);
        }

        public LeagueOfLegendsSummonerService(ILeagueOfLegendsCache leagueOfLegendsCache)
        {
            _leagueOfLegendsCache = leagueOfLegendsCache;
        }

        public async Task<Summoner?> FindSummonerAsync(string summonerName)
        {
            return await RiotApi.Summoner.GetSummonerByNameAsync(Region.Na, summonerName);
        }

        public async Task<Summoner?> FindPendingSummonerAsync(UserId userId)
        {
            return await _leagueOfLegendsCache.SummonerExistsAsync(userId) ? await _leagueOfLegendsCache.GetSummonerAsync(userId) : null;
        }

        public async Task<int> SetPendingSummonerAsync(UserId userId, Summoner summoner)
        {
            await _leagueOfLegendsCache.AddSummonerAsync(userId, summoner);

            return summoner.ProfileIconId;
        }

        public async Task<ValidationResult> ValidatePendingSummonerAsync(UserId userId, Summoner summoner)
        {
            await _leagueOfLegendsCache.RemoveSummonerAsync(userId);

            if (!await this.ValidateProfileIconIdAsync(summoner))
            {
                return new ValidationFailure(string.Empty, "Icon does not match, validation failed.").ToResult();
            }

            return new ValidationResult();
        }

        private async Task<bool> ValidateProfileIconIdAsync(Summoner summoner)
        {
            var currentSummoner = await this.FindSummonerAsync(summoner.Name);

            return currentSummoner!.AccountId == summoner.AccountId && currentSummoner!.ProfileIconId == summoner.ProfileIconId;
        }
    }
}
