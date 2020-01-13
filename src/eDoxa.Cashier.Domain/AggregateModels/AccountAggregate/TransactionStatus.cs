// Filename: TransactionStatus.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionStatus : Enumeration<TransactionStatus>
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1, nameof(Pending));
        public static readonly TransactionStatus Succeeded = new TransactionStatus(1 << 1, nameof(Succeeded));
        public static readonly TransactionStatus Failed = new TransactionStatus(1 << 2, nameof(Failed));
        public static readonly TransactionStatus Canceled = new TransactionStatus(1 << 3, nameof(Canceled));

        public TransactionStatus()
        {
        }

        private TransactionStatus(int value, string name) : base(value, name)
        {
        }
    }
}
