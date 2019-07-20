// Filename: UserProfile.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Api.Areas.Users.ViewModels;
using eDoxa.Identity.Api.Models;

namespace eDoxa.Identity.Api.Areas.Users.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(user => user.Id, config => config.MapFrom(user => user.Id))
                .ForMember(user => user.Gamertag, config => config.MapFrom(user => user.UserName));
        }
    }
}
