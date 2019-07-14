// Filename: RoleModelProfile.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

namespace eDoxa.Identity.Infrastructure.Models.Profiles
{
    internal sealed class RoleModelProfile : Profile
    {
        public RoleModelProfile()
        {
            this.CreateMap<Role, RoleModel>();
        }
    }
}
