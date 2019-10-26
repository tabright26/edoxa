using System.Threading.Tasks;

using FluentValidation.Results;

using RiotSharp.Endpoints.SummonerEndpoint;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions
{
    public interface ISummonerService
    {
        Task<Summoner?> FindSummonerAsync(string summonerName);

        Task<string> GetSummonerValidationIcon(Summoner summoner);

        Task<ValidationResult> ValidateSummonerAsync(Summoner summoner);

    }
}
