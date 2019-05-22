// Filename: FakeController.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.Services.Builders;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/fake/challenges")]
    [ApiExplorerSettings(GroupName = "Fake")]
    public sealed class FakeController : ControllerBase
    {
        private readonly IFakeChallengeService _fakeChallengeService;
        private readonly IMapper _mapper;

        public FakeController(IFakeChallengeService fakeChallengeService, IMapper mapper)
        {
            _fakeChallengeService = fakeChallengeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChallenge(ChallengeType type, bool registerParticipants, bool snapshotParticipantMatches)
        {
            var challenge = await _fakeChallengeService.CreateChallenge(
                new FakeLeagueOfLegendsChallengeBuilder(type),
                registerParticipants,
                snapshotParticipantMatches
            );

            return this.Ok(_mapper.Map<ChallengeDTO>(challenge));
        }
    }
}
