// Filename: ILeagueOfLegendsSummonerService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using RiotSharp.Endpoints.SummonerEndpoint;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions
{
    public interface ILeagueOfLegendsSummonerService
    {
        Task<Summoner?> FindSummonerAsync(string summonerName);

        Task<Summoner?> FindPendingSummonerAsync(UserId userId);

        Task<int> SetPendingSummonerAsync(UserId userId, Summoner summoner);

        Task<ValidationResult> ValidatePendingSummonerAsync(UserId userId, Summoner summoner);
    }
}
