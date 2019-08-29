// Filename: ParticipantResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses.Profiles.Resolvers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses.Profiles
{
    internal sealed class ParticipantResponseProfile : Profile
    {
        public ParticipantResponseProfile()
        {
            this.CreateMap<Participant, ParticipantResponse>()
                .ForMember(participant => participant.Id, config => config.MapFrom<Guid>(participant => participant.Id))
                .ForMember(participant => participant.UserId, config => config.MapFrom<Guid>(participant => participant.UserId))
                .ForMember(participant => participant.Score, config => config.Ignore())
                .ForMember(participant => participant.ChallengeId, config => config.Ignore())
                .ForMember(participant => participant.Matches, config => config.MapFrom(new MatchResponsesResolver(), participant => participant.Matches));
        }
    }
}
