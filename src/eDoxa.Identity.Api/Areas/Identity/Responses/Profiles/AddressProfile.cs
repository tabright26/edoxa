// Filename: AddressProfile.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Areas.Identity.Responses.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<UserAddress, AddressResponse>()
                .ForMember(address => address.Id, config => config.MapFrom(address => address.Id))
                .ForMember(address => address.Type, config => config.MapFrom(address => address.Type))
                .ForMember(address => address.Country, config => config.MapFrom(address => address.Country))
                .ForMember(address => address.Line1, config => config.MapFrom(address => address.Line1))
                .ForMember(address => address.Line2, config => config.MapFrom(address => address.Line2))
                .ForMember(address => address.City, config => config.MapFrom(address => address.City))
                .ForMember(address => address.State, config => config.MapFrom(address => address.State))
                .ForMember(address => address.PostalCode, config => config.MapFrom(address => address.PostalCode));
        }
    }
}
