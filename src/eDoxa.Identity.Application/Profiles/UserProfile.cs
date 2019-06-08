// Filename: UserProfile.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Identity.Application.ViewModels;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Application.Profiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(user => user.Id, config => config.MapFrom(user => user.Id))
                .ForMember(user => user.UserName, config => config.MapFrom(user => user.UserName));
        }
    }
}