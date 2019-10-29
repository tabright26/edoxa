// Filename: CredentialController.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Responses;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/credential")]
    [ApiExplorerSettings(GroupName = "Credential")]
    public sealed class CredentialController : ControllerBase
    {
        private readonly ILeagueOfLegendsSummonerService _summonerService;

        public CredentialController(ILeagueOfLegendsSummonerService summonerService)
        {
            _summonerService = summonerService;
        }

        /// <summary>
        ///     Validate League of Legends summoner's name.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlayerId))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync()
        {
            var userId = HttpContext.GetUserId();

            var summoner = await _summonerService.FindSummonerAsync(userId);

            if (summoner == null)
            {
                return this.NotFound("The user's League of Legends summoner name was not found.");
            }

            var result = await _summonerService.ValidateSummonerAsync(summoner);

            if (result.IsValid)
            {
                return this.Ok(
                    new CredentialResponse
                    {
                        Game = Game.LeagueOfLegends,
                        PlayerId = PlayerId.Parse(summoner.AccountId),
                        UserId = userId
                    });
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
