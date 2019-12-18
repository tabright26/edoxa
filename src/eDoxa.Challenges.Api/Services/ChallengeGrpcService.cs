// Filename: ChallengeGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Application.Profiles;
using eDoxa.Challenges.Api.IntegrationEvents.Extensions;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Responses;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

namespace eDoxa.Challenges.Api.Services
{
    public sealed class ChallengeGrpcService : ChallengeService.ChallengeServiceBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IChallengeQuery _challengeQuery;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeGrpcService(IChallengeService challengeService, IChallengeQuery challengeQuery, IServiceBusPublisher serviceBusPublisher)
        {
            _challengeService = challengeService;
            _challengeQuery = challengeQuery;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public override async Task<FetchChallengesResponse> FetchChallenges(FetchChallengesRequest request, ServerCallContext context)
        {
            var challenges = await _challengeQuery.FetchChallengesAsync(
                request.Game.ToEnumerationOrDefault<Game>(),
                request.State.ToEnumerationOrDefault<ChallengeState>());

            var response = new FetchChallengesResponse
            {
                Challenges =
                {
                    challenges.Select(ChallengeProfile.Map)
                }
            };

            return context.Ok(response);
        }

        public override async Task<FindChallengeResponse> FindChallenge(FindChallengeRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var response = new FindChallengeResponse
            {
                Challenge = ChallengeProfile.Map(challenge)
            };

            return context.Ok(response);
        }

        public override async Task<CreateChallengeResponse> CreateChallenge(CreateChallengeRequest request, ServerCallContext context)
        {
            var result = await _challengeService.CreateChallengeAsync(
                new ChallengeName(request.Name),
                request.Game.ToEnumeration<Game>(),
                new BestOf(request.BestOf),
                new Entries(request.Entries),
                new ChallengeDuration(TimeSpan.FromDays(request.Duration)),
                new UtcNowDateTimeProvider(),
                new Scoring(request.Scoring));

            if (result.IsValid)
            {
                var response = new CreateChallengeResponse
                {
                    Challenge = ChallengeProfile.Map(result.GetEntityFromMetadata<IChallenge>())
                };

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<SynchronizeChallengeResponse> SynchronizeChallenge(SynchronizeChallengeRequest request, ServerCallContext context)
        {
            await _challengeService.SynchronizeChallengesAsync(request.Game.ToEnumeration<Game>(), new UtcNowDateTimeProvider());

            var response = new SynchronizeChallengeResponse();

            return context.Ok(response);
        }

        public override async Task<RegisterChallengeParticipantResponse> RegisterChallengeParticipant(
            RegisterChallengeParticipantRequest request,
            ServerCallContext context
        )
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var challengeId = request.ChallengeId.ParseEntityId<ChallengeId>();

            var participantId = request.ParticipantId.ParseEntityId<ParticipantId>();

            var gamePlayerId = request.GamePlayerId.ParseStringId<PlayerId>();

            var challenge = await _challengeService.FindChallengeOrNullAsync(challengeId);

            if (challenge == null)
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var result = await _challengeService.RegisterChallengeParticipantAsync(
                challenge,
                userId,
                participantId,
                gamePlayerId,
                new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = new RegisterChallengeParticipantResponse
                {
                    Participant = ChallengeProfile.Map(challenge, result.GetEntityFromMetadata<Participant>())
                };

                return context.Ok(response);
            }

            await _serviceBusPublisher.PublishRegisterChallengeParticipantFailedIntegrationEventAsync(challengeId, userId, participantId);

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }
    }
}
