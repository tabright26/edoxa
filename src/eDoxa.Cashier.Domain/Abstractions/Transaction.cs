// Filename: Transaction.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public abstract class Transaction<TCurrency> : Entity<TransactionId>, ITransaction<TCurrency>
    where TCurrency : ICurrency
    {
        protected Transaction(TCurrency amount, TransactionDescription description, TransactionType type) : this()
        {
            Amount = amount;
            Description = description;
            Type = type;
        }

        private Transaction()
        {
            Timestamp = DateTime.UtcNow;
            Status = TransactionStatus.Pending;
            Failure = null;
        }

        public DateTime Timestamp { get; private set; }

        public TCurrency Amount { get; private set; }

        public TransactionDescription Description { get; private set; }

        public TransactionFailure Failure { get; private set; }

        public TransactionType Type { get; private set; }

        public TransactionStatus Status { get; private set; }

        public void Complete()
        {
            Status = TransactionStatus.Completed;
        }

        public void Fail(string message)
        {
            Status = TransactionStatus.Failed;

            Failure = new TransactionFailure(message);
        }
    }
}
