// Filename: CashierMapperFactory.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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

        public static CashierMapperFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

    public sealed partial class CashierMapperFactory : MapperFactory
    {
        protected override IEnumerable<Profile> CreateProfiles()
        {
            yield return new AddressProfile();
            yield return new CardListProfile();
            yield return new CardProfile();
            yield return new CurrencyProfile();
            yield return new MoneyAccountProfile();
        }
    }
}