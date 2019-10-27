// Filename: LeagueOfLegendsSummonersController.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/leagueoflegends/summoners")]
    [ApiExplorerSettings(GroupName = "Summoners")]
    public class LeagueOfLegendsSummonersController : ControllerBase
    {
        private readonly ILeagueOfLegendsSummonerService _leagueOfLegendsSummonerService;

        public LeagueOfLegendsSummonersController(ILeagueOfLegendsSummonerService leagueOfLegendsSummonerService)
        {
            _leagueOfLegendsSummonerService = leagueOfLegendsSummonerService;
        }

        /// <summary>
        ///     Get the required validation summoner icon, start the validation process.
        /// </summary>
        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetByIdAsync(string summonerName)
        {
            var summoner = await _leagueOfLegendsSummonerService.FindSummonerAsync(summonerName);

            if (summoner == null)
            {
                return this.NotFound("Summoner does not exist.");
            }

            var iconId = await _leagueOfLegendsSummonerService.GetSummonerValidationIcon(summoner);

            return this.Ok(iconId);
        }

        /// <summary>
        ///     Validate the summoner, end the validation process.
        /// </summary>
        [HttpPost("{summonerName}")]
        public async Task<IActionResult> PostByIdAsync(string summonerName)
        {
            if (ModelState.IsValid)
            {
                var summoner = await _leagueOfLegendsSummonerService.FindSummonerAsync(summonerName);

                if (summoner == null)
                {
                    return this.NotFound("Summoner does not exist.");
                }

                var result = await _leagueOfLegendsSummonerService.ValidateSummonerAsync(summoner);

                if (result.IsValid)
                {
                    return this.Ok("Validation success. Summoner ID linked.");
                }

                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);
        }
    }
}
