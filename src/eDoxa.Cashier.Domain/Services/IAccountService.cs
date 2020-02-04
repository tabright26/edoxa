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
            EnumCurrencyType currencyType,
            bool includeDisabled = false
        );

        Task<DomainValidationResult<IAccount>> CreateAccountAsync(UserId userId);

        Task<IAccount> FindAccountAsync(UserId userId);

        Task<IAccount?> FindAccountOrNullAsync(UserId userId);

        Task<bool> AccountExistsAsync(UserId userId);

        Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            int bundleId,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            CurrencyType currencyType,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> CreateTransactionAsync(
            IAccount account,
            Currency currency,
            TransactionType type,
            TransactionMetadata? metadata = null,
            CancellationToken cancellationToken = default
        );

        Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId);

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionId transactionId,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsSucceededAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsFailedAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<ITransaction>> MarkAccountTransactionAsCanceledAsync(
            IAccount account,
            TransactionMetadata metadata,
            CancellationToken cancellationToken = default
        );
    }
}
