// Filename: CustomRoleManager.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.Application.Managers
{
    public sealed class CustomRoleManager : RoleManager<Role>
    {
        public CustomRoleManager(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<CustomRoleManager> logger
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
