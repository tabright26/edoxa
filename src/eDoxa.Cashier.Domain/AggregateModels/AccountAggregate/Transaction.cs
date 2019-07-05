// Filename: Transaction.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public partial class Transaction : Entity<TransactionId>, ITransaction
    {
        public Transaction(
            Currency currency,
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

        public Currency Currency { get; }

        public TransactionDescription Description { get; }

        public TransactionType Type { get; }

        public TransactionStatus Status { get; private set; }

        public void MarkAsSucceded()
        {
            if (Status == TransactionStatus.Pending)
            {
                Status = TransactionStatus.Succeded;
            }
        }

        public void MarkAsFailed()
        {
            if (Status == TransactionStatus.Pending)
            {
                Status = TransactionStatus.Failed;
            }
        }
    }

    public partial class Transaction : IEquatable<ITransaction>
    {
        public bool Equals([CanBeNull] ITransaction transaction)
        {
            return Id.Equals(transaction?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as ITransaction);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
