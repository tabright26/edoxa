// Filename: HasBankAccountSpecification.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Cashier.Domain.Specifications
{
    public sealed class HasBankAccountSpecification : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.BankAccountId != null;
        }
    }
}
