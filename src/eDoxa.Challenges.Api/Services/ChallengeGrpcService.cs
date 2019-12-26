// Filename: ChallengeGrpcService.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
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
            var challengeId = request.ChallengeId.ParseEntityId<ChallengeId>();

            if (!await _challengeService.ChallengeExistsAsync(challengeId))
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            var result = await _challengeService.SynchronizeChallengeAsync(challenge, new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = new SynchronizeChallengeResponse
                {
                    Challenge = ChallengeProfile.Map(result.GetEntityFromMetadata<IChallenge>())
                };

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<RegisterChallengeParticipantResponse> RegisterChallengeParticipant(
            RegisterChallengeParticipantRequest request,
            ServerCallContext context
        )
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var challengeId = request.ChallengeId.ParseEntityId<ChallengeId>();

            if (!await _challengeService.ChallengeExistsAsync(challengeId))
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            var result = await _challengeService.RegisterChallengeParticipantAsync(
                challenge,
                userId,
                request.ParticipantId.ParseEntityId<ParticipantId>(),
                request.GamePlayerId.ParseStringId<PlayerId>(),
                new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = new RegisterChallengeParticipantResponse
                {
                    Participant = ChallengeProfile.Map(challenge, result.GetEntityFromMetadata<Participant>())
                };

                return context.Ok(response);
            }

            await _serviceBusPublisher.PublishRegisterChallengeParticipantFailedIntegrationEventAsync(
                challengeId,
                userId,
                request.ParticipantId.ParseEntityId<ParticipantId>());

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<SnapshotChallengeParticipantResponse> SnapshotChallengeParticipant(
            SnapshotChallengeParticipantRequest request,
            ServerCallContext context
        )
        {
            var challengeId = request.ChallengeId.ParseEntityId<ChallengeId>();

            if (!await _challengeService.ChallengeExistsAsync(challengeId))
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            var result = await _challengeService.SnapshotChallengeParticipantAsync(
                challenge,
                request.GamePlayerId.ParseStringId<PlayerId>(),
                new UtcNowDateTimeProvider(),
                scoring => request.Matches.Select(match => new Match(scoring.Map(match.Stats), new GameUuid(match.GameUuid))).ToImmutableHashSet());

            if (result.IsValid)
            {
                var response = new SnapshotChallengeParticipantResponse
                {
                    Participant = ChallengeProfile.Map(challenge, result.GetEntityFromMetadata<Participant>())
                };

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }
    }
}
