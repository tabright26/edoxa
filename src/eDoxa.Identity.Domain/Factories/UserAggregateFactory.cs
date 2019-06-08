// Filename: UserAggregateFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Identity.Domain.Factories
{
    public sealed class UserAggregateFactory
    {
        private static readonly Lazy<UserAggregateFactory> Lazy = new Lazy<UserAggregateFactory>(() => new UserAggregateFactory());

        public static UserAggregateFactory Instance => Lazy.Value;
    }
}
