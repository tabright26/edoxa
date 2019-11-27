// Filename: UserLogin.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
    }
}
