﻿// Filename: InsufficientMoneySpecification.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Cashier.Domain.Specifications
{
    public sealed class InsufficientMoneySpecification : Specification<AccountMoney>
    {
        private readonly Money _money;

        public InsufficientMoneySpecification(Money money)
        {
            _money = money;
        }

        public override Expression<Func<AccountMoney, bool>> ToExpression()
        {
            return account => account.Balance.Available < _money;
        }
    }
}
