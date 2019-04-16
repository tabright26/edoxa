// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-03-17
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges/{challengeId}/participants")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly ILogger<ChallengeParticipantsController> _logger;
        private readonly IParticipantQueries _queries;
        private readonly IMediator _mediator;

        public ChallengeParticipantsController(ILogger<ChallengeParticipantsController> logger, IParticipantQueries queries, IMediator mediator)
        {
            _logger = logger;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the participants of a challenge.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengeParticipantsAsync))]
        public async Task<IActionResult> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            try
            {
                var participants = await _queries.FindChallengeParticipantsAsync(challengeId);

                if (!participants.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(participants);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Register a participant to a challenge.
        /// </summary>
        [HttpPatch(Name = nameof(RegisterChallengeParticipantAsync))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(
            [FromBody]
            RegisterChallengeParticipantCommand command)
        {
            try
            {
                //command.LinkedAccount = User.FindFirst("").Value; // TODO: Create LinkedAccount service.

                await _mediator.SendCommandAsync(command);

                return this.Ok(string.Empty);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}