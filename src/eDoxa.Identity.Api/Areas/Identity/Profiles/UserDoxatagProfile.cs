﻿// Filename: DoxatagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Responses;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public class UserDoxatagProfile : Profile
    {
        public UserDoxatagProfile()
        {
            this.CreateMap<UserDoxatag, UserDoxatagResponse>()
                .ForMember(doxatag => doxatag.UserId, config => config.MapFrom(doxatag => doxatag.UserId))
                .ForMember(doxatag => doxatag.Name, config => config.MapFrom(doxatag => doxatag.Name))
                .ForMember(doxatag => doxatag.Code, config => config.MapFrom(doxatag => doxatag.Code))
                .ForMember(doxatag => doxatag.Timestamp, config => config.MapFrom(doxatag => doxatag.Timestamp));
        }
    }
}
