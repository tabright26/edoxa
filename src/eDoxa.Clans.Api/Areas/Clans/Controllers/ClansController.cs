// Filename: ClansController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Requests;
using eDoxa.Clans.Responses;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [Route("api/clans")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public sealed class ClansController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public ClansController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get all clans.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClanResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var clans = await _clanService.FetchClansAsync();

            if (!clans.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<ClanResponse>>(clans));
        }

        [HttpPost]
        [SwaggerOperation("Create a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(ClanPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _clanService.CreateClanAsync(userId, request.Name);

            if (result.IsValid)
            {
                return this.Ok("The clan has been created.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("{clanId}")]
        [SwaggerOperation("Get a specific clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClanResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan not found.");
            }

            return this.Ok(_mapper.Map<ClanResponse>(clan));
        }

        [HttpPut]
        [SwaggerOperation("Update a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
