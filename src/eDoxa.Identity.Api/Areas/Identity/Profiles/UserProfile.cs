// Filename: UserProfile.cs
// Date Created: 2019-07-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Infrastructure.Models;

using Profile = AutoMapper.Profile;

namespace eDoxa.Identity.Api.Areas.Identity.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserResponse>()
                .ForMember(user => user.Id, config => config.MapFrom(user => user.Id))
                .ForMember(user => user.Doxatag, config => config.MapFrom(user => user.Doxatag));
        }
    }
}
