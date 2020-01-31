// Filename: ITransaction.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface ITransaction : IEntity<TransactionId>
    {
        DateTime Timestamp { get; }

        Currency Currency { get; }

        Price Price { get; }

        TransactionType Type { get; }

        TransactionStatus Status { get; }

        TransactionDescription Description { get; }
        
        TransactionMetadata Metadata { get; }

        void MarkAsSucceeded();

        void MarkAsFailed();

        void MarkAsCanceled();
    }
}
