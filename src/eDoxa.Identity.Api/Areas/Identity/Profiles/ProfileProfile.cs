// Filename: ProfileProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Areas.Identity.Responses;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            this.CreateMap<Infrastructure.Models.Profile, ProfileResponse>()
                .ForMember(profile => profile.FirstName, config => config.MapFrom(profile => profile.FirstName))
                .ForMember(profile => profile.LastName, config => config.MapFrom(profile => profile.LastName))
                .ForMember(profile => profile.Gender, config => config.MapFrom(profile => profile.Gender))
                .ForMember(profile => profile.BirthDate, config => config.MapFrom(profile => profile.BirthDate));
        }
    }
}
