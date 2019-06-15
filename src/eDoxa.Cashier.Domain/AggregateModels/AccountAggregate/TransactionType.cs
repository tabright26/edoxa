// Filename: TransactionType.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Common.Attributes;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionType : Enumeration<TransactionType>
    {
        [AllowValue(true)] public static readonly TransactionType Deposit = new TransactionType(1 << 0, nameof(Deposit));
        [AllowValue(true)] public static readonly TransactionType Reward = new TransactionType(1 << 1, nameof(Reward));
        [AllowValue(true)] public static readonly TransactionType Charge = new TransactionType(1 << 2, nameof(Charge));
        [AllowValue(true)] public static readonly TransactionType Payout = new TransactionType(1 << 3, nameof(Payout));
        [AllowValue(true)] public static readonly TransactionType Withdraw = new TransactionType(1 << 4, nameof(Withdraw));

        public TransactionType()
        {
        }

        private TransactionType(int value, string name) : base(value, name)
        {
        }
    }
}
