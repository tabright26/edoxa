// Filename: LeagueOfLegendsSummonersController.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/summoners")]
    [ApiExplorerSettings(GroupName = "Summoners")]
    public sealed class SummonersController : ControllerBase
    {
        private readonly ILeagueOfLegendsSummonerService _summonerService;

        public SummonersController(ILeagueOfLegendsSummonerService summonerService)
        {
            _summonerService = summonerService;
        }

        /// <summary>
        ///     Get a random summoner profile icon ID to validate a League of Legends summoner name.
        /// </summary>
        [HttpGet("{summonerName}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetBySummonerNameAsync(string summonerName)
        {
            var summoner = await _summonerService.FindSummonerAsync(summonerName);

            if (summoner == null)
            {
                return this.NotFound($"The summoner name ({summonerName}) was not found.");
            }

            var profileIconId = await _summonerService.GenerateDifferentProfileIconIdAsync(summoner);

            return this.Ok(profileIconId);
        }
    }
}
