// Filename: UserListProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.DTO.Profiles
{
    public sealed class UserListProfile : Profile
    {
        public UserListProfile()
        {
            this.CreateMap<IEnumerable<User>, UserListDTO>().ForMember(list => list.Items, config => config.MapFrom(users => users.ToList()));
        }
    }
}