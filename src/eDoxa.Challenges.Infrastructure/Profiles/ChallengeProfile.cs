// Filename: ChallengeProfile.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Infrastructure.Profiles.Converters;
using eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes;
using eDoxa.Challenges.Infrastructure.Profiles.Resolvers;

namespace eDoxa.Challenges.Infrastructure.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<ChallengeModel, IChallenge>().ConvertUsing(new ChallengeTypeConverter());

            this.CreateMap<IChallenge, ChallengeModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom<string>(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game.Value))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => challenge.Timeline.State.Value))
                .ForMember(challenge => challenge.BestOf, config => config.MapFrom<int>(challenge => challenge.BestOf))
                .ForMember(challenge => challenge.Entries, config => config.MapFrom<int>(challenge => challenge.Entries))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(
                    challenge => challenge.Timeline,
                    config => config.ConvertUsing(new ChallengeTimelineModelConverter(), challenge => challenge.Timeline))
                .ForMember(challenge => challenge.ScoringItems, config => config.ConvertUsing(new ScoringItemModelsConverter(), challenge => challenge.Scoring))
                .ForMember(
                    challenge => challenge.Participants,
                    config => config.MapFrom<ParticipantModelsResolver, IReadOnlyCollection<Participant>>(challenge => challenge.Participants));
        }
    }
}
