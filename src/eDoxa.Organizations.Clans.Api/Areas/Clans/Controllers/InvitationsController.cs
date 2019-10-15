// Filename: InvitationsController.cs
// Date Created: 2019-09-30
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
using eDoxa.Seedwork.Domain.Miscs;

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
        ///     Get invitations.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] ClanId? clanId = null, [FromQuery] UserId? userId = null)
        {
            if (clanId == null && userId == null)
            {
                return this.BadRequest("No query parameter as been provided.");
            }

            if (clanId != null && userId != null)
            {
                return this.BadRequest("Only one query parameter must be provided.");
            }

            var invitations = new List<Invitation>();

            if (clanId != null)
            {
                invitations.AddRange(await _invitationService.FetchInvitationsAsync(clanId));
            }

            if (userId != null)
            {
                invitations.AddRange(await _invitationService.FetchInvitationsAsync(userId));
            }

            if (!invitations.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<InvitationResponse>>(invitations));
        }

        /// <summary>
        ///     Get invitation by id.
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
        ///     Create invitation from a clan to a user.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(InvitationPostRequest request)
        {
            var ownerId = HttpContext.GetUserId();

            var result = await _invitationService.SendInvitationAsync(request.ClanId, request.UserId, ownerId);

            if (result.IsValid)
            {
                return this.Ok("The invitation as been sent.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Accept invitation from a clan.
        /// </summary>
        [HttpPost("{invitationId}")]
        public async Task<IActionResult> PostByIdAsync(InvitationId invitationId)
        {
            var userId = HttpContext.GetUserId();

            var invitation = await _invitationService.FindInvitationAsync(invitationId);

            if (invitation == null)
            {
                return this.NotFound("The invitation was not found.");
            }

            var result = await _invitationService.AcceptInvitationAsync(invitation, userId);

            if (result.IsValid)
            {
                return this.Ok("The invitation has been accepted.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Decline invitation from a clan.
        /// </summary>
        [HttpDelete("{invitationId}")]
        public async Task<IActionResult> DeleteByIdAsync(InvitationId invitationId)
        {
            var userId = HttpContext.GetUserId();

            var invitation = await _invitationService.FindInvitationAsync(invitationId);

            if (invitation == null)
            {
                return this.NotFound("The invitation was not found.");
            }

            var result = await _invitationService.DeclineInvitationAsync(invitation, userId);

            if (result.IsValid)
            {
                return this.Ok("The invitation has been declined.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
