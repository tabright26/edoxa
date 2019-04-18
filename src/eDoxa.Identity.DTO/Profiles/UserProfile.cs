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

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.DTO.Profiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserDTO>()
                .ForMember(user => user.Id, config => config.MapFrom(user => user.Id))
                .ForMember(user => user.CurrentStatus, config => config.MapFrom(user => user.CurrentStatus))
                .ForMember(user => user.PreviousStatus, config => config.MapFrom(user => user.PreviousStatus))
                .ForMember(user => user.StatusChanged, config => config.MapFrom(user => user.StatusChanged));
        }
    }
}