// Filename: IAccountService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task<ValidationResult> DepositAsync(
            IAccount account,
            ICurrency currency,
            string email,
            CancellationToken cancellationToken = default
        );

        Task<ValidationResult> WithdrawalAsync(
            IAccount account,
            ICurrency currency,
            string email,
            CancellationToken cancellationToken = default
        );

        Task<IAccount?> FindUserAccountAsync(UserId userId);
    }
}
