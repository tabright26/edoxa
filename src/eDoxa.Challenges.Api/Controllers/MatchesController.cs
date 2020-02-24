// Filename: MatchesController.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
    [Route("api/matches")]
    [ApiExplorerSettings(GroupName = "Match")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchQuery _matchQuery;
        private readonly IMapper _mapper;

        public MatchesController(IMatchQuery matchQuery, IMapper mapper)
        {
            _matchQuery = matchQuery;
            _mapper = mapper;
        }

        [HttpGet("{matchId}")]
        [SwaggerOperation("Find a match.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeMatchDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> FindMatchAsync(MatchId matchId)
        {
            var match = await _matchQuery.FindMatchAsync(matchId);

            if (match == null)
            {
                return this.NotFound("Match not found.");
            }

            return this.Ok(_mapper.Map<ChallengeMatchDto>(match));
        }
    }
}
