// Filename: UserAggregateFactory.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Factories;

namespace eDoxa.Challenges.Domain.Factories
{
    internal sealed partial class UserAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<UserAggregateFactory> Lazy = new Lazy<UserAggregateFactory>(() => new UserAggregateFactory());

        public static UserAggregateFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

    internal sealed partial class UserAggregateFactory
    {
        public User CreateAdmin()
        {
            return new User(UserId.FromGuid(AdminData.Id));
        }

        public User CreateUser()
        {
            return new User(new UserId());
        }
    }
}