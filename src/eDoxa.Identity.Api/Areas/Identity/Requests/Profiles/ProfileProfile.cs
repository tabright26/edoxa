// Filename: ProfileProfile.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

namespace eDoxa.Identity.Api.Areas.Identity.Requests.Profiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            this.CreateMap<Infrastructure.Models.Profile, ProfilePatchRequest>()
                .ForMember(profile => profile.FirstName, config => config.MapFrom(profile => profile.FirstName))
                .ForMember(profile => profile.LastName, config => config.MapFrom(profile => profile.LastName))
                .ForMember(profile => profile.Gender, config => config.MapFrom(profile => profile.Gender))
                .ForMember(profile => profile.BirthDate, config => config.MapFrom(profile => profile.BirthDate));
        }
    }
}
