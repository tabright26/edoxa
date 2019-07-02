// Filename: CurrencyProfile.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.ViewModels;

namespace eDoxa.Cashier.Api.Application.Profiles
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
