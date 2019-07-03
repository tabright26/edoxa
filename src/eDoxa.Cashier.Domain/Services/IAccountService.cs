// Filename: IAccountService.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.Abstactions;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task DepositAsync(User user, ICurrency currency, CancellationToken cancellationToken = default);

        Task WithdrawalAsync(User user, Money money, CancellationToken cancellationToken = default);
    }
}
