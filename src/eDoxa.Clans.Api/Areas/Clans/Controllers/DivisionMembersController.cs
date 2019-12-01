// Filename: DivisionMembersController.cs
// Date Created: 2019-11-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
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
    [Route("api/clans/{clanId}/divisions/{divisionId}/members")]
    [ApiExplorerSettings(GroupName = "Clans")]
    public class DivisionMembersController : ControllerBase
    {
        private readonly IClanService _clanService;
        private readonly IMapper _mapper;

        public DivisionMembersController(IClanService clanService, IMapper mapper)
        {
            _clanService = clanService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get all members of a specific divisions.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MemberResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(DivisionId divisionId)
        {
            var members = await _clanService.FetchDivisionMembersAsync(divisionId);

            if (!members.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        [HttpPost("{memberId}")]
        [SwaggerOperation("Add a member to a division.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostByIdAsync(ClanId clanId, DivisionId divisionId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.AddMemberToDivisionAsync(
                clan,
                userId,
                divisionId,
                memberId);

            if (result.IsValid)
            {
                return this.Ok("Division created.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{memberId}")]
        [SwaggerOperation("Remove a member from a division.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, DivisionId divisionId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.RemoveMemberFromDivisionAsync(
                clan,
                userId,
                divisionId,
                memberId);

            if (result.IsValid)
            {
                return this.Ok("The division has been removed.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
