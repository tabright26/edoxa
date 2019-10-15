using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation.Results;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoner.Services.Abstractions
{
    public interface ISummonerService
    {
        Task<RiotSharp.Endpoints.SummonerEndpoint.Summoner?> FindSummonerAsync(string summonerName);

        Task<string> GetSummonerValidationIcon(RiotSharp.Endpoints.SummonerEndpoint.Summoner summoner);

        Task<ValidationResult> ValidateSummonerAsync(RiotSharp.Endpoints.SummonerEndpoint.Summoner summoner);

    }
}
