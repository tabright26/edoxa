// Filename: IAccountService.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions
{
    public interface IAccountService
    {
        Task CreateAccountAsync(UserId userId);

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

        Task<ValidationResult> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            Currency currency,
            TransactionId transactionId,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        );

        Task<IAccount?> FindUserAccountAsync(UserId userId);
    }
}
