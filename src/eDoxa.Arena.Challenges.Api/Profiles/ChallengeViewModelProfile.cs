// Filename: ChallengeViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Profiles.Converters;
using eDoxa.Arena.Challenges.Api.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ChallengeViewModelProfile : Profile
    {
        public ChallengeViewModelProfile()
        {
            this.CreateMap<IChallenge, ChallengeViewModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom<string>(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game.Name))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State.Name))
                .ForMember(challenge => challenge.Setup, config => config.MapFrom(challenge => challenge.Setup))
                .ForMember(challenge => challenge.Timeline, config => config.MapFrom(challenge => challenge.Timeline))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(
                    challenge => challenge.Participants,
                    config => config.MapFrom(new ParticipantViewModelsResolver(), challenge => challenge.Participants)
                )
                .ForMember(challenge => challenge.Scoring, config => config.ConvertUsing(new ScoringViewModelConverter(), challenge => challenge.Scoring))
                .ForMember(challenge => challenge.Payout, config => config.ConvertUsing(new PayoutConverter(), challenge => challenge.Payout));
        }
    }
}
