// Filename: UserProfile.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Identity.Api.ViewModels;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Api.Profiles
{
    internal sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(user => user.Id, config => config.MapFrom<Guid>(user => user.Id))
                .ForMember(user => user.Gamertag, config => config.MapFrom<string>(user => user.Gamertag));
        }
    }
}
