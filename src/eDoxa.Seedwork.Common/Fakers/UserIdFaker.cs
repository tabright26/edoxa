﻿// Filename: UserIdFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Seedwork.Common.Fakers
{
    public sealed class UserIdFaker : CustomFaker<UserId>
    {
        public UserIdFaker()
        {
            this.CustomInstantiator(faker => UserId.FromGuid(faker.Random.Guid()));
        }
    }
}
