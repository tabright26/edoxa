// Filename: ParticipantsController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/participants")]
    [ApiExplorerSettings(GroupName = "Participant")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _participantQuery;
        private readonly IMapper _mapper;

        public ParticipantsController(IParticipantQuery participantQuery, IMapper mapper)
        {
            _participantQuery = participantQuery;
            _mapper = mapper;
        }

        [HttpGet("{participantId}")]
        [SwaggerOperation("Find a participant.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ParticipantId participantId)
        {
            var participant = await _participantQuery.FindParticipantAsync(participantId);

            if (participant == null)
            {
                return this.NotFound("Participant not found.");
            }

            return this.Ok(_mapper.Map<ParticipantDto>(participant));
        }
    }
}
