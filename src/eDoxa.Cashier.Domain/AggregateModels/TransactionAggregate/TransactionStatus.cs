// Filename: TransactionStatus.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionStatus : Enumeration<TransactionStatus>
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1 << 0, nameof(Pending));
        public static readonly TransactionStatus Succeded = new TransactionStatus(1 << 1, nameof(Succeded));
        public static readonly TransactionStatus Failed = new TransactionStatus(1 << 2, nameof(Failed));

        public TransactionStatus()
        {
        }

        private TransactionStatus(int value, string name) : base(value, name)
        {
        }
    }
}
