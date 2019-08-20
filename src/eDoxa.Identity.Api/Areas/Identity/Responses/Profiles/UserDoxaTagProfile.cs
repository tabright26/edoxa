// Filename: DoxaTagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Responses.Profiles
{
    public class UserDoxaTagProfile : Profile
    {
        public UserDoxaTagProfile()
        {
            this.CreateMap<UserDoxaTag, UserDoxaTagResponse>()
                .ForMember(doxaTag => doxaTag.UserId, config => config.MapFrom(doxaTag => doxaTag.UserId))
                .ForMember(doxaTag => doxaTag.Name, config => config.MapFrom(doxaTag => doxaTag.Name))
                .ForMember(doxaTag => doxaTag.Code, config => config.MapFrom(doxaTag => doxaTag.Code))
                .ForMember(doxaTag => doxaTag.Timestamp, config => config.MapFrom(doxaTag => doxaTag.Timestamp));
        }
    }
}
