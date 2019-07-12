// Filename: UserProfile.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.ViewModels;

namespace eDoxa.Identity.Api.Profiles
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
