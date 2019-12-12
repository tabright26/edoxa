// Filename: ChallengeGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Responses;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Shared.Enums;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using static eDoxa.Grpc.Protos.Challenges.Dtos.ChallengeDto.Types;
using static eDoxa.Grpc.Protos.Challenges.Dtos.MatchDto.Types;

namespace eDoxa.Challenges.Api.Services
{
    public sealed class ChallengeGrpcService : ChallengeService.ChallengeServiceBase
    {
        private readonly IChallengeService _challengeService;
        private readonly IChallengeQuery _challengeQuery;

        public ChallengeGrpcService(IChallengeService challengeService, IChallengeQuery challengeQuery)
        {
            _challengeService = challengeService;
            _challengeQuery = challengeQuery;
        }

        public override async Task<FetchChallengesResponse> FetchChallenges(FetchChallengesRequest request, ServerCallContext context)
        {
            var game = request.Game == GameDto.None ? null : Game.FromValue((int) request.Game);

            var state = request.State == ChallengeStateDto.None ? null : ChallengeState.FromValue((int) request.State);

            var challenges = await _challengeQuery.FetchChallengesAsync(game, state);

            return context.Ok(
                new FetchChallengesResponse
                {
                    Challenges =
                    {
                        challenges.Select(MapChallenge)
                    }
                });
        }

        public override async Task<FindChallengeResponse> FindChallenge(FindChallengeRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                throw context.RpcException(new Status(StatusCode.NotFound, ""));
            }

            return context.Ok(
                new FindChallengeResponse
                {
                    Challenge = MapChallenge(challenge)
                });
        }

        public override async Task<CreateChallengeResponse> CreateChallenge(CreateChallengeRequest request, ServerCallContext context)
        {
            var result = await _challengeService.CreateChallengeAsync(
                new ChallengeName(request.Name),
                Game.FromValue((int) request.Game),
                new BestOf(request.BestOf),
                new Entries(request.Entries),
                new ChallengeDuration(TimeSpan.FromDays(request.Duration)),
                new UtcNowDateTimeProvider(),
                new Scoring(request.Scoring));

            if (result.IsValid)
            {
                return context.Ok(
                    new CreateChallengeResponse
                    {
                        Challenge = MapChallenge(result.GetEntityFromMetadata<IChallenge>())
                    });
            }

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, ""));
        }

        public override async Task<SynchronizeChallengeResponse> SynchronizeChallenge(SynchronizeChallengeRequest request, ServerCallContext context)
        {
            await _challengeService.SynchronizeChallengesAsync(Game.FromValue((int) request.Game), new UtcNowDateTimeProvider());

            return context.Ok(new SynchronizeChallengeResponse());
        }

        public override async Task<RegisterChallengeParticipantResponse> RegisterChallengeParticipant(
            RegisterChallengeParticipantRequest request,
            ServerCallContext context
        )
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var challenge = await _challengeService.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                throw context.RpcException(new Status(StatusCode.NotFound, "Challenge not found."));
            }

            var result = await _challengeService.RegisterChallengeParticipantAsync(
                challenge,
                userId,
                ParticipantId.Parse(request.ParticipantId),
                PlayerId.Parse(request.GamePlayerId),
                new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                return context.Ok(
                    new RegisterChallengeParticipantResponse
                    {
                        Participant = MapParticipant(challenge, result.GetEntityFromMetadata<Participant>())
                    });
            }

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, ""));
        }

        public static ChallengeDto MapChallenge(IChallenge challenge)
        {
            return new ChallengeDto
            {
                Id = challenge.Id.ToString(),
                Name = challenge.Name,
                Game = (GameDto) challenge.Game.Value,
                State = (ChallengeStateDto) challenge.Timeline.State.Value,
                BestOf = challenge.BestOf,
                Entries = challenge.Entries,
                SynchronizedAt =
                    challenge.SynchronizedAt.HasValue ? DateTime.SpecifyKind(challenge.SynchronizedAt.Value, DateTimeKind.Utc).ToTimestamp() : null,
                Timeline = new TimelineDto
                {
                    CreatedAt = DateTime.SpecifyKind(challenge.Timeline.CreatedAt, DateTimeKind.Utc).ToTimestamp(),
                    StartedAt = challenge.Timeline.StartedAt.HasValue
                        ? DateTime.SpecifyKind(challenge.Timeline.StartedAt.Value, DateTimeKind.Utc).ToTimestamp()
                        : null,
                    EndedAt = challenge.Timeline.EndedAt.HasValue
                        ? DateTime.SpecifyKind(challenge.Timeline.EndedAt.Value, DateTimeKind.Utc).ToTimestamp()
                        : null,
                    ClosedAt = challenge.Timeline.ClosedAt.HasValue
                        ? DateTime.SpecifyKind(challenge.Timeline.ClosedAt.Value, DateTimeKind.Utc).ToTimestamp()
                        : null
                },
                Scoring =
                {
                    challenge.Scoring.ToDictionary(scoring => scoring.Key.ToString(), scoring => Convert.ToSingle(scoring.Value))
                },
                Participants =
                {
                    challenge.Participants.Select(participant => MapParticipant(challenge, participant))
                }
            };
        }

        public static ParticipantDto MapParticipant(IChallenge challenge, Participant participant)
        {
            return new ParticipantDto
            {
                Id = participant.Id.ToString(),
                ChallengeId = challenge.Id.ToString(),
                UserId = participant.UserId.ToString(),
                Score = Convert.ToDouble(participant.ComputeScore(challenge.BestOf)?.ToDecimal() ?? 0M),
                Matches =
                {
                    participant.Matches.Select(match => MapMatch(participant, match))
                }
            };
        }

        public static MatchDto MapMatch(Participant participant, IMatch match)
        {
            return new MatchDto
            {
                Id = match.Id.ToString(),
                ParticipantId = participant.Id.ToString(),
                Score = Convert.ToDouble(match.Score.ToDecimal()),
                Stats =
                {
                    match.Stats.Select(MapStat)
                }
            };
        }

        public static StatDto MapStat(Stat stat)
        {
            return new StatDto
            {
                Name = stat.Name,
                Value = stat.Value,
                Weighting = stat.Weighting,
                Score = stat.Score.ToDecimal()
            };
        }
    }
}
