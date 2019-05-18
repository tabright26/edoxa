// Filename: DailyTokenDepositUnavailableSpecification.cs
// Date Created: 2019-05-18
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

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.Specifications
{
    public sealed class DailyTokenDepositUnavailableSpecification : Specification<TokenAccount>
    {
        public override Expression<Func<TokenAccount, bool>> ToExpression()
        {
            return account => account.LastDeposit.HasValue && account.LastDeposit.Value.AddDays(1) >= DateTime.UtcNow;
        }
    }
}