// Filename: RoleRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eDoxa.Identity.Infrastructure.Repositories
{
    public class RoleRepository : RoleStore<Role, IdentityDbContext, Guid, UserRole, RoleClaim>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}