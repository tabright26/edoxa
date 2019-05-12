// Filename: BankAccountProfile.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using Stripe;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            this.CreateMap<BankAccount, BankAccountDTO>()
                .ForMember(card => card.Id, config => config.MapFrom(card => card.Id))
                .ForMember(card => card.BankName, config => config.MapFrom(card => card.BankName))
                .ForMember(card => card.Last4, config => config.MapFrom(card => card.Last4))
                .ForMember(card => card.Status, config => config.MapFrom(card => card.Status));
        }
    }
}