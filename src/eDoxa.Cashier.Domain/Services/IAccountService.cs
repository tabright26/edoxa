// Filename: IAccountService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task<IReadOnlyCollection<TransactionBundleDto>> FetchTransactionBundlesAsync(
            EnumTransactionType type,
            EnumCurrency currency,
            bool includeDisabled = false
        );

        Task<IDomainValidationResult> CreateAccountAsync(UserId userId);

        Task<IAccount> FindAccountAsync(UserId userId);

        Task<IAccount?> FindAccountOrNullAsync(UserId userId);

        Task<bool> AccountExistsAsync(UserId userId);

        Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            int bundleId,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            Currency currency,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            ICurrency currency,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId);

        Task<IDomainValidationResult> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );
    }
}
