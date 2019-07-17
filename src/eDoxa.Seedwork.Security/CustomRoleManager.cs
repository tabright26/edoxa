// Filename: CustomRoleManager.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Security.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.Security
{
    public sealed class CustomRoleManager : RoleManager<RoleModel>
    {
        public CustomRoleManager(
            IRoleStore<RoleModel> store,
            IEnumerable<IRoleValidator<RoleModel>> roleValidators,
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
