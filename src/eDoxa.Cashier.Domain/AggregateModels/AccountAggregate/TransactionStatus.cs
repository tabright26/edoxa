// Filename: TransactionStatus.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionStatus : Enumeration<TransactionStatus>
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1 << 0, nameof(Pending));
        public static readonly TransactionStatus Completed = new TransactionStatus(1 << 1, nameof(Completed));
        public static readonly TransactionStatus Failed = new TransactionStatus(1 << 2, nameof(Failed));

        public TransactionStatus()
        {
        }

        private TransactionStatus(int value, string name) : base(value, name)
        {
        }
    }
}
