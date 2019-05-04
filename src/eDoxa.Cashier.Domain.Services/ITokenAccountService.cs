// Filename: ITokenAccountService.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

namespace eDoxa.Cashier.Domain.Services
{
    public interface ITokenAccountService
    {
        Task<ITokenTransaction> TransactionAsync(UserId userId, CustomerId customerId, TokenBundle bundle, CancellationToken cancellationToken = default);
    }
}