// Filename: ClansController.cs
// Date Created: 2019-09-29
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/leagueoflegends/summoners")]
    [ApiExplorerSettings(GroupName = "Summoners")]
    public class SummonerController : ControllerBase
    {
        private readonly ISummonerService _summonerService;
        private readonly IMapper _mapper;

        public SummonerController(ISummonerService summonerService, IMapper mapper)
        {
            _summonerService = summonerService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get the required validation summoner icon, start the validation process.
        /// </summary>
        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetByIdAsync(string summonerName)
        {
            var summoner  = await _summonerService.FindSummonerAsync(summonerName);

            if (summoner == null)
            {
                return this.NotFound("Summoner does not exist.");
            }

            var iconId = await _summonerService.GetSummonerValidationIcon(summoner);

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
                var summoner = await _summonerService.FindSummonerAsync(summonerName);

                if (summoner == null)
                {
                    return this.NotFound("Summoner does not exist.");
                }

                var result = await _summonerService.ValidateSummonerAsync(summoner);

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
