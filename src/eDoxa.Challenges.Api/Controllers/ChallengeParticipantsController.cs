// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.Queries;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _participantQuery;
        private readonly IMapper _mapper;

        public ChallengeParticipantsController(IParticipantQuery participantQuery, IMapper mapper)
        {
            _participantQuery = participantQuery;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find the participants of a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FetchChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await _participantQuery.FetchChallengeParticipantsAsync(challengeId);

            if (!participants.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IReadOnlyCollection<ParticipantDto>>(participants));
        }
    }
}
