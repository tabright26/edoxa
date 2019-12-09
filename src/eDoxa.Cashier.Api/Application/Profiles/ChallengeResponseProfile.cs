// Filename: ChallengeResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class ChallengeResponseProfile : Profile
    {
        public ChallengeResponseProfile()
        {
            this.CreateMap<IChallenge, ChallengeResponse>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.EntryFee, config => config.MapFrom(challenge => challenge.EntryFee))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => challenge.Payout));
        }
    }
}
