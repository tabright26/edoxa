﻿// Filename: UserRole.cs
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

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    /// <summary>
    ///     Represents the link between a user and a role in the system.
    /// </summary>
    public class UserRole : IdentityUserRole<Guid>
    {
    }
}