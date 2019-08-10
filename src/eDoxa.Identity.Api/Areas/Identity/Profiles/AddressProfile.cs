// Filename: AddressProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Infrastructure.Models;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            this.CreateMap<Address, AddressResponse>()
                .ForMember(address => address.Street, config => config.MapFrom(address => address.Street))
                .ForMember(address => address.City, config => config.MapFrom(address => address.City))
                .ForMember(address => address.PostalCode, config => config.MapFrom(address => address.PostalCode))
                .ForMember(address => address.Country, config => config.MapFrom(address => address.Country));
        }
    }
}
