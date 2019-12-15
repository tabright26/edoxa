// Filename: ClanMembersController.cs
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
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [Route("api/clans/{clanId}/members")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class ClanMembersController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public ClanMembersController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get all members of a specific clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MemberDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync(ClanId clanId)
        {
            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan not found.");
            }

            var members = await _clanService.FetchMembersAsync(clan);

            if (!members.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<MemberDto>>(members));
        }

        [HttpDelete]
        [SwaggerOperation("User leave the clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(ClanId clanId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.LeaveClanAsync(clan, userId);

            if (result.IsValid)
            {
                return this.Ok("The user has left his clan.");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{memberId}")]
        [SwaggerOperation("Kick a specific member from the clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.KickMemberFromClanAsync(clan, userId, memberId);

            if (result.IsValid)
            {
                return this.Ok("The user has been kicked from this clan.");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
