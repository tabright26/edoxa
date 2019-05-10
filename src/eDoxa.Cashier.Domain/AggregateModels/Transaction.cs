// Filename: Transaction.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public abstract class Transaction<TCurrency> : Entity<TransactionId>, ITransaction<TCurrency>
    where TCurrency : ICurrency
    {
        private TCurrency _amount;
        private Option<TransactionDescription> _description;
        private bool _pending;
        private string _serviceId;
        private DateTime _timestamp;
        private TransactionType _type;

        protected Transaction(TCurrency amount, string serviceId) : this()
        {
            _amount = amount;
            _serviceId = serviceId;
            _pending = true;
        }

        protected Transaction(TCurrency amount) : this()
        {
            _amount = amount;
            _serviceId = null;
            _pending = false;
        }

        private Transaction()
        {
            _type = TransactionType.Service;
            _description = new Option<TransactionDescription>();
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public TCurrency Amount => _amount;

        public string ServiceId => _serviceId;

        public bool Pending => _pending;

        public TransactionType Type => _type;

        public Option<TransactionDescription> Description => _description;

        protected void Complete()
        {
            _pending = false;
        }
    }
}