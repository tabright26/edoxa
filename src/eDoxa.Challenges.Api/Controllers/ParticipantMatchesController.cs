// Filename: ParticipantMatchesController.cs
// Date Created: 2019-03-18
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
    [Route("api/participants/{participantId}/matches")]
    public class ParticipantMatchesController : ControllerBase
    {
        private readonly ILogger<ParticipantMatchesController> _logger;
        private readonly IMatchQueries _queries;

        public ParticipantMatchesController(ILogger<ParticipantMatchesController> logger, IMatchQueries queries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        ///     Find the matches of a participant.
        /// </summary>
        [HttpGet(Name = nameof(FindParticipantMatchesAsync))]
        public async Task<IActionResult> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            try
            {
                var matches = await _queries.FindParticipantMatchesAsync(participantId);

                if (!matches.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(matches);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.ParticipantMatchesController_Error_FindParticipantMatchesAsync);
            }

            return this.BadRequest(Resources.ParticipantMatchesController_BadRequest_FindParticipantMatchesAsync);
        }
    }
}