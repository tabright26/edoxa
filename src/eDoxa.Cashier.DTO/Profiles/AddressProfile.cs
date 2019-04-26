// Filename: AddressProfile.cs
// Date Created: 2019-04-21
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
    internal sealed class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Address, AddressDTO>()
                .ForMember(address => address.City, config => config.MapFrom(address => address.City))
                .ForMember(address => address.Country, config => config.MapFrom(address => address.Country))
                .ForMember(address => address.Line1, config => config.MapFrom(address => address.Line1))
                .ForMember(address => address.Line2, config => config.MapFrom(address => address.Line2))
                .ForMember(address => address.PostalCode, config => config.MapFrom(address => address.PostalCode))
                .ForMember(address => address.State, config => config.MapFrom(address => address.State));
        }
    }
}