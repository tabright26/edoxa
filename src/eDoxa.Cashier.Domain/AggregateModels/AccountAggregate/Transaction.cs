﻿// Filename: Transaction.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public partial class Transaction : Entity<TransactionId>, ITransaction
    {
        public Transaction(
            Currency currency,
            TransactionDescription description,
            TransactionType type,
            IDateTimeProvider provider,
            TransactionMetadata? metadata = null
        )
        {
            Timestamp = provider.DateTime;
            Currency = currency;
            Description = description;
            Type = type;
            Status = TransactionStatus.Pending;
            Metadata = metadata ?? new TransactionMetadata();
        }

        public DateTime Timestamp { get; }

        public Currency Currency { get; }

        public Price Price => new Price(Currency);

        public TransactionDescription Description { get; }

        public TransactionMetadata Metadata { get; }

        public TransactionType Type { get; }

        public TransactionStatus Status { get; private set; }

        public void Delete()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Deleted;
        }

        public void MarkAsSucceeded()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Succeeded;
        }

        public void MarkAsFailed()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Failed;
        }

        public void MarkAsCanceled()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Canceled;
        }
    }

    public partial class Transaction : IEquatable<ITransaction?>
    {
        public bool Equals(ITransaction? transaction)
        {
            return Id.Equals(transaction?.Id);
        }

        public sealed override bool Equals(object? obj)
        {
            return this.Equals(obj as ITransaction);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
