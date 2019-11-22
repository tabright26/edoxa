// Filename: ClanLogoController.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clans/{clanId}/logo")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClanLogoController : ControllerBase
    {
        private readonly IClanService _clanService;

        public ClanLogoController(IClanService clanService)
        {
            _clanService = clanService;
        }

        /// <summary>
        ///     Download clan logo.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan doesn't exist.");
            }

            var logo = await _clanService.DownloadLogoAsync(clan);

            if (logo.Length == 0)
            {
                return this.NoContent();
            }

            return this.File(logo, "application/octet-stream");
        }

        /// <summary>
        ///     Upload clan logo.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClanId clanId, [FromForm] IFormFile logo)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.UploadLogoAsync(clan, userId, logo.OpenReadStream(), logo.FileName);

            if (result.IsValid)
            {
                return this.Ok("The logo has been uploaded.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
