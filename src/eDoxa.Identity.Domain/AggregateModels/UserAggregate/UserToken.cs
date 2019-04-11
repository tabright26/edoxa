﻿// Filename: UserToken.cs
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
    ///     Represents an authentication token for a user in the system.
    /// </summary>
    public class UserToken : IdentityUserToken<Guid>
    {
    }
}