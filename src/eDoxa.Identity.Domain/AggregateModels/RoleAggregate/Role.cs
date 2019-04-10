// Filename: Role.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.RoleAggregate
{
    /// <summary>
    ///     Represents a role in the system.
    /// </summary>
    public class Role : IdentityRole<Guid>
    {
        public Role(string name) : base(name)
        {
        }

        private Role()
        {
        }
    }
}