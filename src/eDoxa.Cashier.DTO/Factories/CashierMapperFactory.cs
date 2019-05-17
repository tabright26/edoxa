// Filename: CashierMapperFactory.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.AutoMapper.Factories;
using eDoxa.Cashier.DTO.Profiles;

namespace eDoxa.Cashier.DTO.Factories
{
    public sealed partial class CashierMapperFactory
    {
        private static readonly Lazy<CashierMapperFactory> Lazy = new Lazy<CashierMapperFactory>(() => new CashierMapperFactory());

        public static CashierMapperFactory Instance => Lazy.Value;
    }

    public sealed partial class CashierMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new AccountProfile();
            yield return new StripeCardProfile();
            yield return new StripeCardListProfile();
            yield return new TransactionProfile();
            yield return new TransactionListProfile();
        }
    }
}