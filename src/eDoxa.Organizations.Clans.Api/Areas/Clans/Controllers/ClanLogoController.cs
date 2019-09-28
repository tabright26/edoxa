// Filename: ClanLogoController.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Extensions;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clans/{clanId}/logo")]
    [ApiExplorerSettings(GroupName = "ClanLogo")]
    public class ClanLogoController : ControllerBase
    {
        private readonly IClanService _clanService;

        public ClanLogoController(IClanService clanService)
        {
            _clanService = clanService;
        }

        /// <summary>
        /// Get the logo of a specific clan.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync(ClanId clanId)
        {
            var image = await _clanService.GetClanLogoAsync(clanId);

            if (image == null)
            {
                return this.NoContent();
            }
            return this.Ok(image);
        }

        /// <summary>
        /// Change or update the logo of a specific clan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClanId clanId, [FromBody] FileStream logo)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();
                var clan = await _clanService.FindClanAsync(clanId);

                if (clan == null)
                {
                    return this.NotFound("Clan does not exist.");
                }

                var result = await _clanService.CreateOrUpdateClanLogoAsync(clan, logo, userId);

                if (result.IsValid)
                {
                    return this.Ok("The logo has been updated.");
                }
                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);
        }
    }
}
