// Filename: ChallengesController.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Security;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _query;
        private readonly IFakeChallengeService _fakeChallengeService;
        private readonly IMapper _mapper;

        public ChallengesController(IChallengeQuery query, IFakeChallengeService fakeChallengeService, IMapper mapper)
        {
            _query = query;
            _fakeChallengeService = fakeChallengeService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Find the challenges.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengesAsync))]
        public async Task<IActionResult> FindChallengesAsync([FromQuery] Game game)
        {
            var challenges = await _query.FindChallengesAsync(game);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Create a challenge - Admin only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("money", Name = nameof(CreateMoneyChallenge))]
        public async Task<IActionResult> CreateMoneyChallenge([FromQuery] string name, [FromQuery] Game game, [FromQuery] BestOf bestOf, [FromQuery] PayoutEntries payoutEntries, [FromQuery] MoneyEntryFee entryFee, [FromQuery] bool equivalentCurrency = true, [FromQuery] bool registerParticipants = false, [FromQuery] bool snapshotParticipantMatches = false)
        {
            var challenge = await _fakeChallengeService.CreateChallenge(new ChallengeName(name), game, bestOf, payoutEntries, entryFee, equivalentCurrency, registerParticipants, snapshotParticipantMatches);

            return this.Ok(_mapper.Map<ChallengeDTO>(challenge));
        }

        /// <summary>
        ///     Create a challenge - Admin only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("token", Name = nameof(CreateTokenChallenge))]
        public async Task<IActionResult> CreateTokenChallenge([FromQuery] string name, [FromQuery] Game game, [FromQuery] BestOf bestOf, [FromQuery] PayoutEntries payoutEntries, [FromQuery] TokenEntryFee entryFee, [FromQuery] bool equivalentCurrency = true, [FromQuery] bool registerParticipants = false, [FromQuery] bool snapshotParticipantMatches = false)
        {
            var challenge = await _fakeChallengeService.CreateChallenge(new ChallengeName(name), game, bestOf, payoutEntries, entryFee, equivalentCurrency, registerParticipants, snapshotParticipantMatches);

            return this.Ok(_mapper.Map<ChallengeDTO>(challenge));
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [HttpGet("{challengeId}", Name = nameof(FindChallengeAsync))]
        public async Task<IActionResult> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _query.FindChallengeAsync(challengeId);

            return challenge
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("Challenge not found."))
                .Single();
        }
    }
}