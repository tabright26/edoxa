// Filename: Transaction.cs
// Date Created: 2019-05-13
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
        private TCurrency _amount;
        private TransactionDescription _description;
        private TransactionFailure _failure;
        private TransactionStatus _status;
        private DateTime _timestamp;
        private TransactionType _type;

        protected Transaction(TCurrency amount, TransactionDescription description, TransactionType type) : this()
        {
            _amount = amount;
            _description = description;
            _type = type;
        }

        private Transaction()
        {
            _timestamp = DateTime.UtcNow;
            _status = TransactionStatus.Pending;
            _failure = null;
        }

        public DateTime Timestamp => _timestamp;

        public TCurrency Amount => _amount;

        public TransactionDescription Description => _description;

        public TransactionFailure Failure => _failure;

        public TransactionType Type => _type;

        public TransactionStatus Status => _status;

        public void Complete()
        {
            _status = TransactionStatus.Completed;
        }

        public void Fail(string message)
        {
            _status = TransactionStatus.Failed;

            _failure = new TransactionFailure(message);
        }
    }
}
