// Filename: ClanDivisionsController.cs
// Date Created: 2019-10-31
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
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

        /// <summary>
        ///     Get all members of a specific divisions.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync(DivisionId divisionId)
        {
            var members = await _clanService.FetchDivisionMembersAsync(divisionId);

            if (!members.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        /// <summary>
        ///     Add a member to a division.
        /// </summary>
        [HttpPost("{memberId}")]
        public async Task<IActionResult> PostByIdAsync(ClanId clanId, DivisionId divisionId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.AddMemberToDivisionAsync(clan, userId, divisionId, memberId);

            if (result.IsValid)
            {
                return this.Ok("Division created.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Remove a member from a division.
        /// </summary>
        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, DivisionId divisionId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.RemoveMemberFromDivisionAsync(clan, userId, divisionId, memberId);

            if (result.IsValid)
            {
                return this.Ok("The division has been removed.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

    }
}
