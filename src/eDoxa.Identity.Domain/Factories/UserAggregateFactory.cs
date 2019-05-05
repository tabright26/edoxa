// Filename: UserAggregateFactory.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Factories;

namespace eDoxa.Identity.Domain.Factories
{
    public sealed partial class UserAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<UserAggregateFactory> Lazy = new Lazy<UserAggregateFactory>(() => new UserAggregateFactory());

        public static UserAggregateFactory Instance => Lazy.Value;
    }

    public sealed partial class UserAggregateFactory
    {
        //public User CreateAdmin()
        //{
        //    return User.Create(AdminData);
        //}

        //public User CreateFrancis()
        //{
        //    return User.Create(FrancisData);
        //}

        //public User CreateRoy()
        //{
        //    return User.Create(RoyData);
        //}

        //public User CreateRyan()
        //{
        //    return User.Create(RyanData);
        //}
    }
}