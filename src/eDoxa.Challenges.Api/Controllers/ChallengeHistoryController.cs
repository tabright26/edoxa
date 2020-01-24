// Filename: ChallengeHistoryController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/history")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeHistoryController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IMapper _mapper;

        public ChallengeHistoryController(IChallengeQuery challengeQuery, IMapper mapper)
        {
            _challengeQuery = challengeQuery;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find the challenge history of a user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Game? game = null, ChallengeState? state = null)
        {
            var userId = HttpContext.GetUserId();

            var challenges = await _challengeQuery.FetchUserChallengeHistoryAsync(userId, game, state);

            if (!challenges.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IReadOnlyCollection<ChallengeDto>>(challenges));
        }
    }
}
