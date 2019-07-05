// Filename: CurrencyProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Api.Application.Profiles.Converters;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            this.CreateMap<CurrencyViewModel, ICurrency>().ConvertUsing(new CurrencyConverter());

            this.CreateMap<ICurrency, CurrencyViewModel>()
                .ForMember(currency => currency.Type, config => config.MapFrom(currency => currency.Type))
                .ForMember(currency => currency.Amount, config => config.MapFrom(currency => currency.Amount));
        }
    }
}
