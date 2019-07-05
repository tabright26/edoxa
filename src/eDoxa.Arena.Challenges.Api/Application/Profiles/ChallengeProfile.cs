// Filename: ChallengeProfile.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Application.Profiles.Converters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<ChallengeModel, ChallengeViewModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => ChallengeGame.FromValue(challenge.Game).Name))
                .ForMember(challenge => challenge.State, config => config.MapFrom(challenge => ChallengeState.FromValue(challenge.State).Name))
                .ForMember(challenge => challenge.Setup, config => config.MapFrom(challenge => challenge.Setup))
                .ForMember(challenge => challenge.Timeline, config => config.MapFrom(challenge => challenge.Timeline))
                .ForMember(challenge => challenge.SynchronizedAt, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants))
                .ForMember(challenge => challenge.Scoring, config => config.ConvertUsing(new ScoringConverter(), challenge => challenge.ScoringItems))
                .ForMember(challenge => challenge.Payout, config => config.ConvertUsing(new PayoutConverter(), challenge => challenge.Buckets));

            this.CreateMap<ChallengeTimelineModel, ChallengeTimelineViewModel>()
                .ForMember(timeline => timeline.CreatedAt, config => config.MapFrom(timeline => timeline.CreatedAt))
                .ForMember(timeline => timeline.StartedAt, config => config.MapFrom(timeline => timeline.StartedAt))
                .ForMember(timeline => timeline.EndedAt, config => config.MapFrom(timeline => timeline.StartedAt + TimeSpan.FromTicks(timeline.Duration)))
                .ForMember(timeline => timeline.ClosedAt, config => config.MapFrom(timeline => timeline.ClosedAt));

            this.CreateMap<ChallengeSetupModel, ChallengeSetupViewModel>()
                .ForMember(setup => setup.BestOf, config => config.MapFrom(setup => setup.BestOf))
                .ForMember(setup => setup.Entries, config => config.MapFrom(setup => setup.Entries))
                .ForMember(setup => setup.PayoutEntries, config => config.MapFrom(setup => setup.PayoutEntries))
                .ForMember(
                    setup => setup.EntryFee,
                    config => config.MapFrom(
                        setup => new EntryFeeViewModel
                        {
                            Currency = Currency.FromValue(setup.EntryFeeCurrency).Name,
                            Amount = setup.EntryFeeAmount
                        }
                    )
                );
        }
    }
}
