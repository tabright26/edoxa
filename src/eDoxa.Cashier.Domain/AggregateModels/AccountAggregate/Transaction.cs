// Filename: Transaction.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public class Transaction : Entity<TransactionId>, ITransaction
    {
        protected Transaction(Currency amount, TransactionDescription description, TransactionType type) : this()
        {
            Currency = amount;
            Description = description;
            Type = type;
        }

        private Transaction()
        {
            Timestamp = DateTime.UtcNow;
            Status = TransactionStatus.Pending;
            Failure = null;
        }

        public Currency Currency { get; private set; }

        public DateTime Timestamp { get; private set; }

        public TransactionType Type { get; private set; }

        public TransactionStatus Status { get; private set; }

        public TransactionDescription Description { get; private set; }

        public TransactionFailure Failure { get; private set; }

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
