// Filename: CandidaturesController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Services;
using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Clans.Api.Controllers
{
    [Authorize]
    [Route("api/candidatures")]
    [ApiExplorerSettings(GroupName = "Candidatures")]
    public sealed class CandidaturesController : ControllerBase
    {
        private readonly ICandidatureService _candidatureService;
        private readonly IMapper _mapper;

        public CandidaturesController(ICandidatureService candidatureService, IMapper mapper)
        {
            _candidatureService = candidatureService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Get candidatures.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CandidatureDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetAsync([FromQuery] ClanId? clanId = null, [FromQuery] UserId? userId = null)
        {
            // TODO: Use ValidationProblemDetails to handle bad request.
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

            return this.Ok(_mapper.Map<IEnumerable<CandidatureDto>>(candidatures));
        }

        [HttpGet("{candidatureId}")]
        [SwaggerOperation("Get a specific candidature from the Id.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CandidatureDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(CandidatureId candidatureId)
        {
            var candidature = await _candidatureService.FindCandidatureAsync(candidatureId);

            if (candidature == null)
            {
                return this.NotFound("Candidature not found.");
            }

            return this.Ok(_mapper.Map<CandidatureDto>(candidature));
        }

        [HttpPost]
        [SwaggerOperation("Create candidature from a user to a clan.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(SendCandidatureRequest request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _candidatureService.SendCandidatureAsync(userId, request.ClanId.ParseEntityId<ClanId>());

            if (result.IsValid)
            {
                return this.Ok("The candidature as been sent.");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("{candidatureId}")]
        [SwaggerOperation("Accept candidature from a user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{candidatureId}")]
        [SwaggerOperation("Decline candidature from a user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
