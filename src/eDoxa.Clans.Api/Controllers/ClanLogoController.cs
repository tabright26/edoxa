﻿// Filename: ClanLogoController.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.IO;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Clans.Api.Controllers
{
    [Authorize]
    [Route("api/clans/{clanId}/logo")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClanLogoController : ControllerBase
    {
        private readonly IClanService _clanService;

        public ClanLogoController(IClanService clanService)
        {
            _clanService = clanService;
        }

        [HttpGet]
        [SwaggerOperation("Download clan logo.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Stream))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DownloadLogoAsync(ClanId clanId)
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation("Upload clan logo.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UploadLogoAsync(ClanId clanId, [FromForm] IFormFile logo)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.UploadLogoAsync(
                clan,
                userId,
                logo.OpenReadStream(),
                logo.FileName);

            if (result.IsValid)
            {
                return this.Ok("The logo has been uploaded.");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
