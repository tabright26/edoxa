// Filename: IAccount.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface IAccount : IEntity<UserId>, IAggregateRoot
    {
        IReadOnlyCollection<ITransaction> Transactions { get; }

        Balance GetBalanceFor(Currency currency);

        void CreateTransaction(ITransaction transaction);

        bool TransactionExists(TransactionId transactionId);

        ITransaction FindTransaction(TransactionId transactionId);
    }
}
