// Filename: UserRepository.cs
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
    public class UserRepository : UserStore<User, Role, IdentityDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>, IUserRepository
    {
        public UserRepository(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}