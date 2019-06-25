// Filename: ChallengeProfile.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.Converters;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.Resolvers;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
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
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(
                    challenge => challenge.Timeline,
                    config => config.ConvertUsing(new ChallengeTimelineModelConverter(), challenge => challenge.Timeline)
                )
                .ForMember(challenge => challenge.Setup, config => config.ConvertUsing(new ChallengeSetupModelConverter(), challenge => challenge.Setup))
                .ForMember(challenge => challenge.ScoringItems, config => config.ConvertUsing(new ScoringItemModelsConverter(), challenge => challenge.Scoring))
                .ForMember(challenge => challenge.Buckets, config => config.ConvertUsing(new BucketModelsConverter(), challenge => challenge.Payout))
                .ForMember(
                    challenge => challenge.Participants,
                    config => config.MapFrom<ParticipantModelsResolver, IReadOnlyCollection<Participant>>(challenge => challenge.Participants)
                );
        }
    }
}
