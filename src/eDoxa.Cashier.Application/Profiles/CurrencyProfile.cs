// Filename: CurrencyProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Application.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Application.Profiles
{
    internal sealed class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            this.CreateMap<Currency, CurrencyViewModel>()
                .ForMember(currency => currency.Type, config => config.MapFrom(currency => currency.Type))
                .ForMember(currency => currency.Amount, config => config.MapFrom(currency => currency.Amount));

            this.CreateMap<CurrencyViewModel, Currency>().ConvertUsing(currency => Currency.Convert(currency.Amount, currency.Type));
        }
    }
}
