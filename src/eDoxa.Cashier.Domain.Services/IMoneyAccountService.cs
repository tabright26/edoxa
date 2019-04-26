// Filename: IAccountService.cs
// Date Created: 2019-04-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IMoneyAccountService
    {
        Task TransactionAsync<TCurrency>(User user, Bundle<TCurrency> bundle, CancellationToken cancellationToken = default)
        where TCurrency : ICurrency;
    }
}