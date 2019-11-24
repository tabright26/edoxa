// Filename: ClanDivisionsController.cs
// Date Created: 2019-10-31
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Api.Areas.Clans.Requests;
using eDoxa.Clans.Api.Areas.Clans.Responses;
using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/clans/{clanId}/divisions")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClanDivisionsController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public ClanDivisionsController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get all divisions of a specific clan.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync(ClanId clanId)
        {
            var divisions = await _clanService.FetchDivisionsAsync(clanId);

            if (!divisions.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<DivisionResponse>>(divisions));
        }

        /// <summary>
        ///     Create a division.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClanId clanId, DivisionPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.CreateDivisionAsync(clan, userId, request.Name, request.Description);

            if (result.IsValid)
            {
                return this.Ok("Division created.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        /// <summary>
        ///     Remove a specific division.
        /// </summary>
        [HttpDelete("{divisionId}")]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, DivisionId divisionId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.DeleteDivisionAsync(clan, userId, divisionId);

            if (result.IsValid)
            {
                return this.Ok("The division has been removed.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        /// <summary>
        ///     Update a division.
        /// </summary>
        [HttpPost("{divisionId}")]
        public async Task<IActionResult> UpdateByIdAsync(ClanId clanId, DivisionId divisionId, DivisionPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.UpdateDivisionAsync(clan, userId, divisionId, request.Name, request.Description);

            if (result.IsValid)
            {
                return this.Ok("Division Updated.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

    }
}
