// Filename: InsufficientMoneySpecification.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Specifications;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate.Specifications
{
    public sealed class InsufficientMoneySpecification : Specification<MoneyAccount>
    {
        private readonly Money _money;

        public InsufficientMoneySpecification(Money money)
        {
            _money = money;
        }

        public override Expression<Func<MoneyAccount, bool>> ToExpression()
        {
            return account => account.Balance < _money;
        }
    }
}