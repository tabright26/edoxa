// Filename: EntryFeeResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class EntryFeeResponseProfile : Profile
    {
        public EntryFeeResponseProfile()
        {
            this.CreateMap<EntryFee, EntryFeeResponse>()
                .ForMember(entryFee => entryFee.Currency, config => config.MapFrom(entryFee => entryFee.Currency.Name))
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount));
        }
    }
}
