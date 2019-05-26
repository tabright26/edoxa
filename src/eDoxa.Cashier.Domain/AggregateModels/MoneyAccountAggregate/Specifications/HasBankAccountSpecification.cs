// Filename: HasBankAccountSpecification.cs
// Date Created: 2019-05-25
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
    public class HasBankAccountSpecification : Specification<MoneyAccount>
    {
        public override Expression<Func<MoneyAccount, bool>> ToExpression()
        {
            return account => account.User.BankAccountId != null;
        }
    }
}
