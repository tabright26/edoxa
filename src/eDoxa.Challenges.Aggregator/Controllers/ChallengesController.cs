﻿// Filename: ChallengesController.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Challenges.Aggregator.IntegrationEvents.Extensions;
using eDoxa.Challenges.Aggregator.Models;
using eDoxa.Challenges.Aggregator.Requests;
using eDoxa.Challenges.Aggregator.Services;
using eDoxa.Challenges.Aggregator.Transformers;
using eDoxa.Identity.Responses;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Refit;

using Swashbuckle.AspNetCore.Annotations;

using CashierRequests = eDoxa.Cashier.Requests;
using ChallengeRequests = eDoxa.Challenges.Requests;

namespace eDoxa.Challenges.Aggregator.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IChallengesService _challengesService;
        private readonly ICashierService _cashierService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengesController(
            IIdentityService identityService,
            IChallengesService challengesService,
            ICashierService cashierService,
            IServiceBusPublisher serviceBusPublisher
        )
        {
            _identityService = identityService;
            _challengesService = challengesService;
            _cashierService = cashierService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FetchChallengesAsync()
        {
            var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

            var challengesFromCashierService = await _cashierService.FetchChallengesAsync();

            var challengesFromChallengesService = await _challengesService.FetchChallengesAsync();

            return this.Ok(ChallengeTransformer.Transform(challengesFromChallengesService, challengesFromCashierService, doxatagsFromIdentityService));
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateChallengeAsync([FromBody] CreateChallengeRequest request)
        {
            var challengeId = new ChallengeId();

            try
            {
                var challengeFromChallengesService = await _challengesService.CreateChallengeAsync(
                    new ChallengeRequests.CreateChallengeRequest(
                        challengeId,
                        request.Name,
                        request.Game,
                        request.BestOf,
                        request.Entries,
                        request.Duration));

                var challengeFromCashierService = await _cashierService.CreateChallengeAsync(
                    new CashierRequests.CreateChallengeRequest(
                        challengeId,
                        challengeFromChallengesService.Entries / 2,
                        request.EntryFeeAmount,
                        request.EntryFeeCurrency));

                return this.Ok(
                    ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, new Collection<UserDoxatagResponse>()));
            }
            catch (ApiException exception)
            {
                await _serviceBusPublisher.PublishChallengeCreationFailedIntegrationEventAsync(challengeId);

                return this.BadRequest(JsonConvert.DeserializeObject<ValidationProblemDetails>(exception.Content));
            }
        }

        [HttpGet("{challengeId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FindChallengeAsync(Guid challengeId)
        {
            var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

            var challengeFromCashierService = await _cashierService.FindChallengeAsync(challengeId);

            var challengeFromChallengesService = await _challengesService.FindChallengeAsync(challengeId);

            return this.Ok(ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService));
        }

        //[HttpPost("{challengeId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //public async Task<IActionResult> SynchronizeChallengeAsync(Guid challengeId)
        //{
        //    var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

        //    var challengeFromCashierService = await _cashierService.FindChallengeAsync(challengeId);

        //    var challengeFromChallengesService = await _challengesService.SynchronizeChallengeAsync(challengeId);

        //    return this.Ok(ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService));
        //}
    }
}
