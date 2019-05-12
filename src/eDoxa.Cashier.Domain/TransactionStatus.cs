// Filename: TransactionStatus.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class TransactionStatus : Enumeration<TransactionStatus>
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1 << 0, nameof(Pending));
        public static readonly TransactionStatus Paid = new TransactionStatus(1 << 1, nameof(Paid));
        public static readonly TransactionStatus Succeeded = new TransactionStatus(1 << 2, nameof(Succeeded));
        public static readonly TransactionStatus Canceled = new TransactionStatus(1 << 3, nameof(Canceled));
        public static readonly TransactionStatus Failed = new TransactionStatus(1 << 4, nameof(Failed));

        private TransactionStatus(int value, string name) : base(value, name)
        {
        }
    }
}