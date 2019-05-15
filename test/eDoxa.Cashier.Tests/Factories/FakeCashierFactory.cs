// Filename: FakeCashierFactory.cs
// Date Created: 2019-05-13
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

namespace eDoxa.Cashier.Tests.Factories
{
    public sealed class FakeCashierFactory
    {
        private static readonly Lazy<FakeCashierFactory> Lazy = new Lazy<FakeCashierFactory>(() => new FakeCashierFactory());

        public static FakeCashierFactory Instance => Lazy.Value;

        public UserId CreateUserId()
        {
            return new UserId();
        }

        public IBundle CreateBundle()
        {
            return new MoneyBundle(Money.OneHundred);
        }

        public ITransaction CreateTransaction()
        {
            return new DepositMoneyTransaction(Money.OneHundred);
        }

        public Money CreateMoney()
        {
            return Money.OneHundred;
        }

        public Token CreateToken()
        {
            return Token.OneHundredThousand;
        }
    }
}