// Filename: Transaction.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public partial class Transaction : Entity<TransactionId>, ITransaction
    {
        public Transaction(
            ICurrency currency,
            TransactionDescription description,
            TransactionType type,
            IDateTimeProvider provider
        )
        {
            Timestamp = provider.DateTime;
            Currency = currency;
            Description = description;
            Type = type;
            Status = TransactionStatus.Pending;
        }

        public DateTime Timestamp { get; }

        public ICurrency Currency { get; }

        public Price Price => new Price(Currency);

        public TransactionDescription Description { get; }

        public TransactionType Type { get; }

        public TransactionStatus Status { get; private set; }

        public void MarkAsSucceded()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Succeded;
        }

        public void MarkAsFailed()
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new InvalidOperationException();
            }

            Status = TransactionStatus.Failed;
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
