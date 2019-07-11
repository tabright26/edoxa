// Filename: ChallengeModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles
{
    internal sealed class ChallengeModelProfile : Profile
    {
        public ChallengeModelProfile()
        {
            this.CreateMap<IChallenge, ChallengeModel>()
                .ForMember(challenge => challenge.Id, config => config.MapFrom<Guid>(challenge => challenge.Id))
                .ForMember(challenge => challenge.EntryFeeCurrency, config => config.MapFrom(challenge => challenge.EntryFee.Currency.Value))
                .ForMember(challenge => challenge.EntryFeeAmount, config => config.MapFrom(challenge => challenge.EntryFee.Amount))
                .ForMember(challenge => challenge.Buckets, config => config.ConvertUsing(new BucketModelsConverter(), challenge => challenge.Payout));
        }
    }
}
