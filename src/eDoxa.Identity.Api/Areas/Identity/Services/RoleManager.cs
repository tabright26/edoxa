// Filename: CustomRoleManager.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class RoleManager : RoleManager<Role>, IRoleManager
    {
        public RoleManager(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            CustomIdentityErrorDescriber errors,
            ILogger<RoleManager> logger
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
