// Filename: ChallengeResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Challenges.Api.Areas.Challenges.Responses.Profiles.Converters;
using eDoxa.Challenges.Api.Areas.Challenges.Responses.Profiles.Resolvers;
using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.Api.Areas.Challenges.Responses.Profiles
{
    internal sealed class ChallengeResponseProfile : Profile
    {
        public ChallengeResponseProfile()
        {
            this.CreateMap<IChallenge, ChallengeResponse>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom<string>(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game.Name))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State.Name))
                .ForMember(challenge => challenge.BestOf, config => config.MapFrom<int>(challenge => challenge.BestOf))
                .ForMember(challenge => challenge.Entries, config => config.MapFrom<int>(challenge => challenge.Entries))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(challenge => challenge.Timeline, config => config.MapFrom(challenge => challenge.Timeline))
                .ForMember(challenge => challenge.Scoring, config => config.ConvertUsing(new ScoringResponseConverter(), challenge => challenge.Scoring))
                .ForMember(
                    challenge => challenge.Participants,
                    config => config.MapFrom(new ParticipantResponsesResolver(), challenge => challenge.Participants));
        }
    }
}
