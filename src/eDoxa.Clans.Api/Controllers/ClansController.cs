// Filename: ClansController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Domain.Services;
using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Clans.Api.Controllers
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClanDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var clans = await _clanService.FetchClansAsync();

            if (!clans.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<ClanDto>>(clans));
        }

        [HttpPost]
        [SwaggerOperation("Create a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(CreateClanRequest request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _clanService.CreateClanAsync(userId, request.Name);

            if (result.IsValid)
            {
                return this.Ok("The clan has been created.");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("{clanId}")]
        [SwaggerOperation("Get a specific clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClanDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan not found.");
            }

            return this.Ok(_mapper.Map<ClanDto>(clan));
        }

        [HttpPut]
        [SwaggerOperation("Update a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(ClanId clanId, UpdateClanRequest request)
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

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
