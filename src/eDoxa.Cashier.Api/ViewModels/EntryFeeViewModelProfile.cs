// Filename: EntryFeeViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.ViewModels
{
    internal sealed class EntryFeeViewModelProfile : Profile
    {
        public EntryFeeViewModelProfile()
        {
            this.CreateMap<EntryFee, EntryFeeViewModel>()
                .ForMember(entryFee => entryFee.Currency, config => config.MapFrom(entryFee => entryFee.Currency.Name))
                .ForMember(entryFee => entryFee.Amount, config => config.MapFrom(entryFee => entryFee.Amount));
        }
    }
}
