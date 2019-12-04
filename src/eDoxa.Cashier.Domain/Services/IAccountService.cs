// Filename: IAccountService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task CreateAccountAsync(UserId userId);

        Task<IAccount?> FindAccountAsync(UserId userId);

        Task<IDomainValidationResult> CreateTransactionAsync(
            IAccount account,
            decimal amount,
            Currency currency,
            TransactionType transactionType,
            TransactionMetadata? transactionMetadata = null,
            CancellationToken cancellationToken = default
        );

        Task PayoutChallengeAsync(IChallenge challenge, IDictionary<UserId, decimal?> scoreboard, CancellationToken cancellationToken = default);

        Task<ITransaction?> FindAccountTransactionAsync(IAccount account, TransactionId transactionId);

        Task<IDomainValidationResult> MarkAccountTransactionAsSuccededAsync(
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
    }
}
