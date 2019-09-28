// Filename: ClanLogoController.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Responses;
using eDoxa.Organizations.Clans.Api.Extensions;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/candidatures")]
    [ApiExplorerSettings(GroupName = "Candidatures")]
    public class CandidaturesController : ControllerBase
    {
        private readonly ICandidatureService _candidatureService;
        private readonly IMapper _mapper;

        public CandidaturesController(ICandidatureService candidatureService, IMapper mapper)
        {
            _candidatureService = candidatureService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all candidatures from a ClanId.
        /// </summary>
        [HttpGet("byClanId")]
        public async Task<IActionResult> GetByClanIdAsync([FromQuery] ClanId clanId)
        {
            var candidatures = await _candidatureService.FetchCandidaturesAsync(clanId);

            if (!candidatures.Any())
            {
                return this.NoContent();
            }
            return this.Ok(_mapper.Map<IEnumerable<CandidatureResponse>>(candidatures));
        }

        /// <summary>
        /// Get all candidatures from a UserId.
        /// </summary>
        [HttpGet("byUserId")]
        public async Task<IActionResult> GetByUserIdAsync([FromQuery] UserId userId)
        {
            var candidatures = await _candidatureService.FetchCandidaturesAsync(userId);

            if (!candidatures.Any())
            {
                return this.NoContent();
            }
            return this.Ok(_mapper.Map<IEnumerable<CandidatureResponse>>(candidatures));
        }

        /// <summary>
        /// Get a specific candidature from the Id.
        /// </summary>
        [HttpGet("{candidatureId}")]
        public async Task<IActionResult> GetByIdAsync(CandidatureId candidatureId)
        {
            var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

            if (candidature == null)
            {
                return this.NotFound();
            }
            return this.Ok(_mapper.Map<CandidatureResponse>(candidature));
        }

        /// <summary>
        /// Create candidature from a user to a clan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CandidaturePostRequest candidatureRequest)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();

                var clan = await _candidatureService.FindClanAsync(candidatureRequest.ClanId);

                if (clan == null)
                {
                    return this.NotFound("Clan does not exist.");
                }

                var result = await _candidatureService.SendCandidatureAsync(userId, candidatureRequest.ClanId);

                if (result.IsValid)
                {
                    return this.Ok("Candidature sent to clan.");
                }
                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);
        }

        /// <summary>
        /// Accept candidature from a user.
        /// </summary>
        [HttpPost("{candidatureId}")]
        public async Task<IActionResult> PostByIdAsync(CandidatureId candidatureId)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();
                var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

                if (candidature == null)
                {
                    return this.NotFound("Candidature does not exist.");
                }

                var result = await _candidatureService.AcceptCandidatureAsync(userId, candidature);

                if (result.IsValid)
                {
                    return this.Ok("Candidature accepted.");
                }
                result.AddToModelState(ModelState, null);
            }
            return this.BadRequest(ModelState);
        }

        /// <summary>
        /// Decline candidature from a user.
        /// </summary>
        [HttpDelete("{candidatureId}")]
        public async Task<IActionResult> DeleteByIdAsync(CandidatureId candidatureId)
        {
            if (ModelState.IsValid)
            {
                var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

                if (candidature == null)
                {
                    return this.NotFound("Candidature does not exist.");
                }

                var result = await _candidatureService.DeclineCandidatureAsync(candidature);

                if (result.IsValid)
                {
                    return this.Ok("The candidature has been declined.");
                }
                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);

        }
    }
}
