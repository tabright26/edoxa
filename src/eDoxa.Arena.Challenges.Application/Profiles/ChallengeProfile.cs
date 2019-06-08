// Filename: ChallengeProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain;

namespace eDoxa.Arena.Challenges.Application.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<Challenge, ChallengeViewModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id.ToGuid()))
                .ForMember(challenge => challenge.Timestamp, config => config.MapFrom(challenge => challenge.CreatedAt))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name.ToString()))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State))
                .ForMember(challenge => challenge.Timeline, config => config.MapFrom(challenge => challenge.Timeline))
                .ForMember(challenge => challenge.Setup, config => config.MapFrom(challenge => challenge.Setup))
                .ForMember(challenge => challenge.Scoring, config => config.MapFrom(challenge => new Scoring(challenge.Stats)))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => new Payout(new Buckets(challenge.Buckets))))
                .ForMember(challenge => challenge.Scoreboard, config => config.MapFrom(challenge => new Scoreboard(challenge.Participants)))
                .ForMember(
                    challenge => challenge.Participants,
                    config => config.MapFrom(challenge => challenge.Participants.OrderBy(participant => participant.Timestamp))
                );
        }
    }
}
