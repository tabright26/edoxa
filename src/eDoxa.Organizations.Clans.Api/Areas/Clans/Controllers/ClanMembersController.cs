// Filename: ClanMemberController.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
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
    [Route("api/clans/{clanId}/members")]
    [ApiExplorerSettings(GroupName = "ClanMembers")]
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
        public async Task<IActionResult> GetAsync(ClanId clanId)
        {
            var members = await _clanService.FetchMembersAsync(clanId);

            if (!members.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(ClanId clanId)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();
                var clan = await _clanService.FindClanAsync(clanId);

                if (clan == null)
                {
                    return this.NotFound("Clan does not exist.");
                }

                var memberId = clan.Members.SingleOrDefault(member => member.UserId == userId)?.Id;

                if (memberId == null)
                {
                    //Todo check with Frank to see if this is decent
                    return this.Conflict("User not in the clan.");
                }

                var result = await _clanService.LeaveClanAsync(clan, memberId);

                if (result.IsValid)
                {
                    return this.Ok("The user has left his clan.");
                }
                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);
        }

        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, MemberId memberId)
        {
            if (ModelState.IsValid)
            {
                var clan = await _clanService.FindClanAsync(clanId);

                if (clan == null)
                {
                    return this.NotFound("User's does not have a clan.");
                }

                var result = await _clanService.KickMemberFromClanAsync(clan, memberId);

                if (result.IsValid)
                {
                    this.Ok("The user has been kicked from this clan.");
                }
                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);

        }


    }
}
