// Filename: ITransaction.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public interface ITransaction : IEntity<TransactionId>
    {
        DateTime Timestamp { get; }

        ICurrency Currency { get; }

        Price Price { get; }

        TransactionType Type { get; }

        TransactionStatus Status { get; }

        TransactionDescription Description { get; }

        void MarkAsSucceded();

        void MarkAsFailed();
    }
}
