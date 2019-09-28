// Filename: ClanLogoController.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
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
    [Route("api/invitations")]
    [ApiExplorerSettings(GroupName = "Invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        private readonly IMapper _mapper;

        public InvitationsController(IInvitationService invitationService, IMapper mapper)
        {
            _invitationService = invitationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all invitations from a ClanId.
        /// </summary>
        [HttpGet("byClanId")]
        public async Task<IActionResult> GetByClanIdAsync([FromQuery] ClanId clanId)
        {
            var invitations = await _invitationService.FetchInvitationsAsync(clanId);

            if (!invitations.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<InvitationResponse>>(invitations));
        }

        /// <summary>
        /// Get all invitations from a UserId.
        /// </summary>
        [HttpGet("byUserId")]
        public async Task<IActionResult> GetByUserIdAsync([FromQuery] UserId userId)
        {
            var invitations = await _invitationService.FetchInvitationsAsync(userId);

            if (!invitations.Any())
            {
                return this.NoContent();
            }
            return this.Ok(_mapper.Map<IEnumerable<InvitationResponse>>(invitations));
        }

        /// <summary>
        /// Get a specific invitation from the Id.
        /// </summary>
        [HttpGet("{invitationId}")]
        public async Task<IActionResult> GetByIdAsync(InvitationId invitationId)
        {
            var invitation = await _invitationService.FindInvitationAsync(invitationId);

            if (invitation == null)
            {
                return this.NotFound();
            }
            return this.Ok(_mapper.Map<InvitationResponse>(invitation));
        }

        /// <summary>
        /// Create invitation from a clan to a user.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(InvitationPostRequest invitationRequest)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();

                var clan = await _invitationService.FindClanAsync(invitationRequest.ClanId);

                if (clan == null)
                {
                    return this.NotFound("Clan does not exist.");
                }

                var result = await _invitationService.SendInvitationAsync(userId, invitationRequest.ClanId, invitationRequest.UserId);

                if (result.IsValid)
                {
                    return this.Ok("Invitation sent to user.");
                }

                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);
        }

        /// <summary>
        /// Accept invitation from a clan.
        /// </summary>
        [HttpPost("{invitationId}")]
        public async Task<IActionResult> PostByIdAsync(InvitationId invitationId)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();
                var invitation = await _invitationService.FindInvitationAsync(invitationId);

                if (invitation == null)
                {
                    return this.NotFound("Invitation does not exist.");
                }

                var result = await _invitationService.AcceptInvitationAsync(userId, invitation);

                if (result.IsValid)
                {
                    return this.Ok("Invitation accepted.");
                }
                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);
        }

        /// <summary>
        /// Decline invitation from a clan.
        /// </summary>
        [HttpDelete("{invitationId}")]
        public async Task<IActionResult> DeleteByIdAsync(InvitationId invitationId)
        {
            if (ModelState.IsValid)
            {
                var invitation = await _invitationService.FindInvitationAsync(invitationId);

                if (invitation == null)
                {
                    return this.NotFound("Invitation does not exist.");
                }

                var result = await _invitationService.DeclineInvitationAsync(invitation);

                if (result.IsValid)
                {
                    return this.Ok("The invitation has been declined.");
                }
                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);

        }
    }
}
