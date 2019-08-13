// Filename: DoxaTagProfile.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Responses.Profiles
{
    public class DoxaTagProfile : Profile
    {
        public DoxaTagProfile()
        {
            this.CreateMap<DoxaTag, DoxaTagResponse>()
                .ForMember(doxaTag => doxaTag.Name, config => config.MapFrom(doxaTag => doxaTag.Name))
                .ForMember(doxaTag => doxaTag.Code, config => config.MapFrom(doxaTag => doxaTag.Code));
        }
    }
}
