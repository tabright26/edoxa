// Filename: FakeCashierFactory.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Cashier.Tests.Utilities.Fakes
{
    public sealed class FakeCashierFactory
    {
        private static readonly Lazy<FakeCashierFactory> Lazy = new Lazy<FakeCashierFactory>(() => new FakeCashierFactory());

        public static FakeCashierFactory Instance => Lazy.Value;

        public UserId CreateUserId()
        {
            return new UserId();
        }

        public User CreateUser()
        {
            return new User(this.CreateUserId(), new StripeAccountId("acct_qweqwe1231qwe"), new StripeCustomerId("cus_qweqwe1231qwe"));
        }

        public IBundle CreateBundle()
        {
            return new MoneyBundle(Money.OneHundred);
        }

        public ITransaction CreateTransaction()
        {
            return new MoneyDepositTransaction(Money.OneHundred);
        }
    }
}
