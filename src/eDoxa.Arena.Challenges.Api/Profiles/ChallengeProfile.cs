// Filename: ChallengeProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Profiles.Converters;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<ChallengeModel, ChallengeViewModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => ChallengeGame.FromValue(challenge.Game)))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => ChallengeState.FromValue(challenge.State)))
                .ForMember(challenge => challenge.Timeline, config => config.ConvertUsing(new ChallengeTimelineConverter(), challenge => challenge))
                .ForMember(challenge => challenge.Setup, config => config.ConvertUsing(new ChallengeSetupConverter(), challenge => challenge))
                .ForMember(challenge => challenge.Scoring, config => config.ConvertUsing(new ScoringConverter(), challenge => challenge))
                .ForMember(challenge => challenge.Payout, config => config.ConvertUsing(new PayoutConverter(), challenge => challenge))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants));
        }
    }
}
