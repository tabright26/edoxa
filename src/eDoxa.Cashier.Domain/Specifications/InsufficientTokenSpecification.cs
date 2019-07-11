// Filename: InsufficientTokenSpecification.cs
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
    public sealed class InsufficientTokenSpecification : Specification<TokenAccount>
    {
        private readonly Token _token;

        public InsufficientTokenSpecification(Token token)
        {
            _token = token;
        }

        public override Expression<Func<TokenAccount, bool>> ToExpression()
        {
            return account => account.Balance.Available < _token;
        }
    }
}
