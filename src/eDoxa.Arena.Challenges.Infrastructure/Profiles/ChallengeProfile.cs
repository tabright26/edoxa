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
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Infrastructure.Converters;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
{
    internal sealed class ChallengeProfile : Profile
    {
        public ChallengeProfile()
        {
            this.CreateMap<ChallengeModel, IChallenge>().ConvertUsing(new ChallengeConverter());

            this.CreateMap<IChallenge, ChallengeModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.Name, config => config.MapFrom<string>(challenge => challenge.Name))
                .ForMember(challenge => challenge.Game, config => config.MapFrom(challenge => challenge.Game.Value))
                .ForMember(challenge => challenge.Timestamp, config => config.MapFrom(challenge => challenge.CreatedAt))
                .ForMember(challenge => challenge.LastSync, config => config.MapFrom(challenge => challenge.SynchronizedAt))
                .ForMember(
                    challenge => challenge.Setup,
                    config => config.MapFrom(
                        challenge => new SetupModel
                        {
                            BestOf = challenge.Setup.BestOf.Value,
                            Entries = challenge.Setup.Entries.Value,
                            PayoutEntries = challenge.Setup.PayoutEntries.Value,
                            EntryFeeAmount = challenge.Setup.EntryFee.Amount,
                            EntryFeeCurrency = challenge.Setup.EntryFee.Type.Value
                        }
                    )
                )
                .ForMember(
                    challenge => challenge.Timeline,
                    config => config.MapFrom(
                        challenge => new TimelineModel
                        {
                            Duration = challenge.Timeline.Duration.Ticks,
                            StartedAt = challenge.Timeline.StartedAt,
                            ClosedAt = challenge.Timeline.ClosedAt
                        }
                    )
                )
                .ForMember(challenge => challenge.Participants, config => config.MapFrom(challenge => challenge.Participants))
                .ForMember(
                    challenge => challenge.ScoringItems,
                    config => config.MapFrom(
                        challenge => challenge.Scoring.Select(
                            scoring => new ScoringItemModel
                            {
                                Name = scoring.Key,
                                Weighting = scoring.Value
                            }
                        )
                    )
                )
                .ForMember(
                    challenge => challenge.Buckets,
                    config => config.MapFrom(
                        challenge => challenge.Payout.Buckets.Select(
                            bucket => new BucketModel
                            {
                                Size = bucket.Size,
                                PrizeCurrency = bucket.Prize.Type.Value,
                                PrizeAmount = bucket.Prize.Amount
                            }
                        )
                    )
                )
                .ForMember(challenge => challenge.Seed, config => config.Ignore());
        }
    }
}
