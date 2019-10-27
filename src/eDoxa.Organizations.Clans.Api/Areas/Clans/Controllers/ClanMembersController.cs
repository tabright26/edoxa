// Filename: ClanMembersController.cs
// Date Created: 2019-09-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
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

        /// <summary>
        ///     Get all members of a specific clan.
        /// </summary>
        [HttpGet]
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

            return this.Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        /// <summary>
        ///     User leave the clan.
        /// </summary>
        [HttpDelete]
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

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Kick a specific member from the clan.
        /// </summary>
        [HttpDelete("{memberId}")]
        public async Task<IActionResult> DeleteByIdAsync(ClanId clanId, MemberId memberId)
        {
            var userId = HttpContext.GetUserId();

            var clan = await _clanService.FindClanAsync(clanId);

            if (clan == null)
            {
                return this.NotFound("Clan does not exist.");
            }

            var result = await _clanService.KickMemberFromClanAsync(userId, clan, memberId);

            if (result.IsValid)
            {
                return this.Ok("The user has been kicked from this clan.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
