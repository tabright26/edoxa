// Filename: DoxatagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Infrastructure.Models;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public class DoxatagProfile : Profile
    {
        public DoxatagProfile()
        {
            this.CreateMap<Doxatag, DoxatagResponse>()
                .ForMember(doxatag => doxatag.Prefix, config => config.MapFrom(doxatag => doxatag.Name))
                .ForMember(doxatag => doxatag.Suffix, config => config.MapFrom(doxatag => doxatag.UniqueTag))
                .ForMember(doxatag => doxatag.Value, config => config.MapFrom(doxatag => doxatag.ToString()));
        }
    }
}
