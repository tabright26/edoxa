// Filename: ClansController.cs
// Date Created: 2019-09-29
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
    [Route("api/clans")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClansController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public ClansController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get all clans.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var clans = await _clanService.FetchClansAsync();

            if (!clans.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<ClanResponse>>(clans));
        }

        /// <summary>
        ///     Create a clan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(ClanPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _clanService.CreateClanAsync(userId, request.Name);

            if (result.IsValid)
            {
                return this.Ok("The clan has been created.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Update a clan.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ClanId clanId, ClanPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.UpdateClanAsync(clan, userId, request.Summary);

            if (result.IsValid)
            {
                return this.Ok("Clan updated.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Get a specific clan.
        /// </summary>
        [HttpGet("{clanId}")]
        public async Task<IActionResult> GetByIdAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound();
            }

            return this.Ok(_mapper.Map<ClanResponse>(clan));
        }
    }
}
