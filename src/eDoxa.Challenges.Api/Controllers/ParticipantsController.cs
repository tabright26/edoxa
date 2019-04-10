// Filename: ParticipantsController.cs
// Date Created: 2019-03-17
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Properties;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly ILogger<ParticipantsController> _logger;
        private readonly IParticipantQueries _queries;

        public ParticipantsController(ILogger<ParticipantsController> logger, IParticipantQueries queries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        ///     Find a participant.
        /// </summary>
        [HttpGet("{participantId}", Name = nameof(FindParticipantAsync))]
        public async Task<IActionResult> FindParticipantAsync(ParticipantId participantId)
        {
            try
            {
                var participant = await _queries.FindParticipantAsync(participantId);

                if (participant == null)
                {
                    return this.NotFound(Resources.ParticipantsController_NotFound_FindParticipantAsync);
                }

                return this.Ok(participant);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.ParticipantsController_Error_FindParticipantAsync);
            }

            return this.BadRequest(Resources.ParticipantsController_BadRequest_FindParticipantAsync);
        }
    }
}