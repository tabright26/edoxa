// Filename: ILeagueOfLegendsSummonerService.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using FluentValidation.Results;

using RiotSharp.Endpoints.SummonerEndpoint;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions
{
    public interface ILeagueOfLegendsSummonerService
    {
        Task<Summoner?> FindSummonerAsync(string summonerName);

        Task<string> GetSummonerValidationIcon(Summoner summoner);

        Task<ValidationResult> ValidateSummonerAsync(Summoner summoner);
    }
}
