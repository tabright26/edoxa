// Filename: ParticipantViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ParticipantViewModelProfile : Profile
    {
        public ParticipantViewModelProfile()
        {
            this.CreateMap<Participant, ParticipantViewModel>()
                .ForMember(participant => participant.Id, config => config.MapFrom<Guid>(participant => participant.Id))
                .ForMember(participant => participant.UserId, config => config.MapFrom<Guid>(participant => participant.UserId))
                .ForMember(participant => participant.Score, config => config.Ignore())
                .ForMember(participant => participant.ChallengeId, config => config.Ignore())
                .ForMember(participant => participant.Matches, config => config.MapFrom(new MatchViewModelsResolver(), participant => participant.Matches));
        }
    }
}
