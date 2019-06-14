// Filename: PrizePoolProfile.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class PrizePoolProfile : Profile
    {
        public PrizePoolProfile()
        {
            this.CreateMap<PrizePool, PrizePoolViewModel>()
                .ForMember(prizePool => prizePool.Amount, config => config.MapFrom(prizePool => prizePool.Amount))
                .ForMember(prizePool => prizePool.Type, config => config.MapFrom(prizePool => prizePool.Type));
        }
    }
}
