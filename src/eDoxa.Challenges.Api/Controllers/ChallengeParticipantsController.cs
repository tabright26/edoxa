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

using eDoxa.Challenges.Api.Properties;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
                _logger.LogError(exception, Resources.ChallengeParticipantsController_Error_FindChallengeParticipantsAsync);
            }

            return this.BadRequest(Resources.ChallengeParticipantsController_BadRequest_FindChallengeParticipantsAsync);
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

                return this.Ok(Resources.ChallengeParticipantsController_Ok_RegisterChallengeParticipantAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.ChallengeParticipantsController_Error_RegisterChallengeParticipantAsync);
            }

            return this.BadRequest(Resources.ChallengeParticipantsController_BadRequest_RegisterChallengeParticipantAsync);
        }
    }
}