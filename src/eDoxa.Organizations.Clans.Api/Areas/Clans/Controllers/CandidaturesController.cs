// Filename: CandidaturesController.cs
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
        ///     Get candidatures.
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

            var candidatures = new List<Candidature>();

            if (clanId != null)
            {
                candidatures.AddRange(await _candidatureService.FetchCandidaturesAsync(clanId));
            }

            if (userId != null)
            {
                candidatures.AddRange(await _candidatureService.FetchCandidaturesAsync(userId));
            }

            if (!candidatures.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<CandidatureResponse>>(candidatures));
        }

        /// <summary>
        ///     Get a specific candidature from the Id.
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
        ///     Create candidature from a user to a clan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CandidaturePostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _candidatureService.SendCandidatureAsync(userId, request.ClanId);

            if (result.IsValid)
            {
                return this.Ok("The candidature as been sent.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Accept candidature from a user.
        /// </summary>
        [HttpPost("{candidatureId}")]
        public async Task<IActionResult> PostByIdAsync(CandidatureId candidatureId)
        {
            var ownerId = HttpContext.GetUserId();

            var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

            if (candidature == null)
            {
                return this.NotFound("The candidature was not found.");
            }

            var result = await _candidatureService.AcceptCandidatureAsync(candidature, ownerId);

            if (result.IsValid)
            {
                return this.Ok("The candidature has been accepted.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Decline candidature from a user.
        /// </summary>
        [HttpDelete("{candidatureId}")]
        public async Task<IActionResult> DeleteByIdAsync(CandidatureId candidatureId)
        {
            var ownerId = HttpContext.GetUserId();

            var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

            if (candidature == null)
            {
                return this.NotFound("The candidature was not found.");
            }

            var result = await _candidatureService.DeclineCandidatureAsync(candidature, ownerId);

            if (result.IsValid)
            {
                return this.Ok("The candidature has been declined.");
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
