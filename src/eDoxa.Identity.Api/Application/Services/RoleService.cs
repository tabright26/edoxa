// Filename: CustomRoleManager.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class RoleService : RoleManager<Role>, IRoleService
    {
        public RoleService(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleService> logger
        ) : base(
            store,
            roleValidators,
            keyNormalizer,
            errors,
            logger
        )
        {
        }
    }
}
