// Filename: WeeklyMoneyWithdrawUnavailableSpecification.cs
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

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Specifications;

namespace eDoxa.Cashier.Domain.Specifications
{
    public sealed class WeeklyMoneyWithdrawUnavailableSpecification : Specification<IMoneyAccount>
    {
        public override Expression<Func<IMoneyAccount, bool>> ToExpression()
        {
            return account => account.LastWithdraw.HasValue && account.LastWithdraw.Value.AddDays(7) >= DateTime.UtcNow;
        }
    }
}
