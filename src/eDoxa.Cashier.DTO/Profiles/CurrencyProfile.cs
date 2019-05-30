// Filename: CurrencyProfile.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            this.CreateMap<Currency, CurrencyDTO>()
                .ForMember(currency => currency.Type, config => config.MapFrom(currency => currency.Type))
                .ForMember(currency => currency.Amount, config => config.MapFrom(currency => currency.Amount));
        }
    }
}
