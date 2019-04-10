// Filename: UserProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
                .ForMember(user => user.Username, configuration => configuration.MapFrom(user => user.Tag.Name))
                .ForMember(user => user.Tag, configuration => configuration.MapFrom(user => user.Tag.ReferenceNumber));
        }
    }
}