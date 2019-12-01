// Filename: TransactionStatus.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Misc
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionStatus : Enumeration<TransactionStatus>
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1, nameof(Pending));
        public static readonly TransactionStatus Succeded = new TransactionStatus(1 << 1, nameof(Succeded));
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
