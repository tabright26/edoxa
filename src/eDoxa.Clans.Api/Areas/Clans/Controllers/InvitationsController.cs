// Filename: InvitationsController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.Models;
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

        [HttpGet]
        [SwaggerOperation("Get invitations.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(InvitationResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
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

        [HttpGet("{invitationId}")]
        [SwaggerOperation("Get invitation by id.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(InvitationResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(InvitationId invitationId)
        {
            var invitation = await _invitationService.FindInvitationAsync(invitationId);

            if (invitation == null)
            {
                return this.NotFound();
            }

            return this.Ok(_mapper.Map<InvitationResponse>(invitation));
        }

        [HttpPost]
        [SwaggerOperation("Create invitation from a clan to a user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(InvitationPostRequest request)
        {
            var ownerId = HttpContext.GetUserId();

            var result = await _invitationService.SendInvitationAsync(ClanId.FromGuid(request.ClanId), UserId.FromGuid(request.UserId), ownerId);

            if (result.IsValid)
            {
                return this.Ok("The invitation as been sent.");
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("{invitationId}")]
        [SwaggerOperation("Accept invitation from a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{invitationId}")]
        [SwaggerOperation("Decline invitation from a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
