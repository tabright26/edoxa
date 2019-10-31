// Filename: AuthenticateController.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.LeagueOfLegends.Api.Areas.Summoners.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
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
    [Route("api/authenticate")]
    [ApiExplorerSettings(GroupName = "Authenticate")]
    public sealed class AuthenticateController : ControllerBase
    {
        private readonly ILeagueOfLegendsSummonerService _summonerService;

        public AuthenticateController(ILeagueOfLegendsSummonerService summonerService)
        {
            _summonerService = summonerService;
        }

        /// <summary>
        ///     Authenticate League of Legends summoner's.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlayerId))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync()
        {
            var userId = HttpContext.GetUserId();

            var summoner = await _summonerService.FindPendingSummonerAsync(userId);

            if (summoner == null)
            {
                return this.NotFound("Pending authentication was not found.");
            }

            var result = await _summonerService.ValidatePendingSummonerAsync(userId, summoner);

            if (result.IsValid)
            {
                return this.Ok(PlayerId.Parse(summoner!.AccountId));
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
