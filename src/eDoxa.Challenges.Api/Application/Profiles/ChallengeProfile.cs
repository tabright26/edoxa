// Filename: ChallengeProfile.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Api.Application.Profiles.Converters;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.Domain.Extensions;

using Google.Protobuf.WellKnownTypes;

using static eDoxa.Grpc.Protos.Challenges.Dtos.ChallengeDto.Types;
using static eDoxa.Grpc.Protos.Challenges.Dtos.MatchDto.Types;

namespace eDoxa.Challenges.Api.Application.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<Stat, StatDto>()
                .ForMember(stat => stat.Name, config => config.MapFrom<string>(stat => stat.Name))
                .ForMember(stat => stat.Value, config => config.MapFrom<double>(stat => stat.Value))
                .ForMember(stat => stat.Weighting, config => config.MapFrom<float>(stat => stat.Weighting))
                .ForMember(stat => stat.Score, config => config.MapFrom<DecimalValue>(stat => stat.Score.ToDecimal()));

            this.CreateMap<IMatch, MatchDto>()
                .ForMember(match => match.Id, config => config.MapFrom(match => match.Id.ToString()))
                .ForMember(match => match.ChallengeId, config => config.Ignore())
                .ForMember(match => match.GameUuid, config => config.MapFrom(match => match.GameUuid.ToString()))
                .ForMember(match => match.GameStartedAt, config => config.MapFrom(match => match.GameStartedAt.ToTimestampUtc()))
                .ForMember(match => match.GameDuration, config => config.MapFrom(match => match.GameDuration.ToDuration()))
                .ForMember(match => match.GameEndedAt, config => config.MapFrom(match => match.GameEndedAt.ToTimestampUtc()))
                .ForMember(match => match.SynchronizedAt, config => config.MapFrom(match => match.SynchronizedAt.ToTimestampUtc()))
                .ForMember(match => match.Score, config => config.MapFrom<DecimalValue>(match => match.Score.ToDecimal()))
                .ForMember(match => match.ParticipantId, config => config.Ignore())
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats));

            this.CreateMap<Participant, ParticipantDto>()
                .ForMember(participant => participant.Id, config => config.MapFrom(participant => participant.Id.ToString()))
                .ForMember(participant => participant.UserId, config => config.MapFrom(participant => participant.UserId.ToString()))
                .ForMember(participant => participant.Score, config => config.Ignore())
                .ForMember(participant => participant.ChallengeId, config => config.Ignore())
                .ForMember(participant => participant.GamePlayerId, config => config.MapFrom(participant => participant.PlayerId.ToString()))
                .ForMember(
                    participant => participant.SynchronizedAt,
                    config => config.MapFrom(participant => participant.SynchronizedAt.ToTimestampUtcOrNull()))
                .ForMember(participant => participant.Matches, config => config.MapFrom(participant => participant.Matches));

            this.CreateMap<ChallengeTimeline, TimelineDto>()
                .ForMember(timeline => timeline.CreatedAt, config => config.MapFrom(timeline => timeline.CreatedAt.ToTimestampUtc()))
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt.ToTimestampUtcOrNull()))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.EndedAt.ToTimestampUtcOrNull()))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt.ToTimestampUtcOrNull()))
                .ForMember(timeline => timeline.Duration, config => config.MapFrom(timeline => TimeSpan.FromTicks(timeline.Duration.Ticks).ToDuration()));

            this.CreateMap<IChallenge, ChallengeDto>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id.ToString()))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name.ToString()))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game.ToEnum<EnumGame>()))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State.ToEnum<EnumChallengeState>()))
                .ForMember(challenge => challenge.BestOf, config => config.MapFrom<int>(challenge => challenge.BestOf))
                .ForMember(challenge => challenge.Entries, config => config.MapFrom<int>(challenge => challenge.Entries))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt.ToTimestampUtcOrNull()))
                .ForMember(challenge => challenge.Timeline, config => config.MapFrom(challenge => challenge.Timeline))
                .ForMember(challenge => challenge.Scoring, config => config.ConvertUsing(new ScoringConverter(), challenge => challenge.Scoring))
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants))
                .ForMember(challenge => challenge.Type, config => config.Ignore());
        }

        public static ChallengeDto Map(IChallenge challenge)
        {
            return new ChallengeDto
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Game = challenge.Game.ToEnum<EnumGame>(),
                State = challenge.Timeline.State.ToEnum<EnumChallengeState>(),
                BestOf = challenge.BestOf,
                Entries = challenge.Entries,
                SynchronizedAt = challenge.SynchronizedAt.ToTimestampUtcOrNull(),
                Timeline = new TimelineDto
                {
                    CreatedAt = challenge.Timeline.CreatedAt.ToTimestampUtc(),
                    StartedAt = challenge.Timeline.StartedAt.ToTimestampUtcOrNull(),
                    EndedAt = challenge.Timeline.EndedAt.ToTimestampUtcOrNull(),
                    ClosedAt = challenge.Timeline.ClosedAt.ToTimestampUtcOrNull(),
                    Duration = TimeSpan.FromTicks(challenge.Timeline.Duration.Ticks).ToDuration()
                },
                Scoring =
                {
                    challenge.Scoring.ToDictionary(scoring => scoring.Key.ToString(), scoring => scoring.Value.ToSingle())
                },
                Participants =
                {
                    challenge.Participants.Select(participant => Map(challenge, participant))
                }
            };
        }

        public static ParticipantDto Map(IChallenge challenge, Participant participant)
        {
            return new ParticipantDto
            {
                Id = participant.Id,
                UserId = participant.UserId,
                GamePlayerId = participant.PlayerId,
                ChallengeId = challenge.Id,
                Score = participant.ComputeScore(challenge.BestOf)?.ToDecimal(),
                SynchronizedAt = participant.SynchronizedAt.ToTimestampUtcOrNull(),
                Matches =
                {
                    participant.Matches.Select(match => Map(challenge, participant, match))
                }
            };
        }

        private static MatchDto Map(IChallenge challenge, Participant participant, IMatch match)
        {
            return new MatchDto
            {
                Id = match.Id,
                ChallengeId = challenge.Id,
                ParticipantId = participant.Id,
                GameUuid = match.GameUuid,
                GameStartedAt = match.GameStartedAt.ToTimestampUtc(),
                GameDuration = match.GameDuration.ToDuration(),
                GameEndedAt = match.GameEndedAt.ToTimestampUtc(),
                SynchronizedAt = match.SynchronizedAt.ToTimestampUtc(),
                Score = match.Score.ToDecimal(),
                Stats =
                {
                    match.Stats.Select(Map)
                }
            };
        }

        private static StatDto Map(Stat stat)
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
