﻿// Filename: TransactionType.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class TransactionType : Enumeration<TransactionType>
    {
        public static readonly TransactionType Deposit = new TransactionType(1 << 0, nameof(Deposit));
        public static readonly TransactionType Reward = new TransactionType(1 << 1, nameof(Reward));
        public static readonly TransactionType Charge = new TransactionType(1 << 2, nameof(Charge));
        public static readonly TransactionType Payout = new TransactionType(1 << 3, nameof(Payout));
        public static readonly TransactionType Withdrawal = new TransactionType(1 << 4, nameof(Withdrawal));

        public TransactionType()
        {
        }

        private TransactionType(int value, string name) : base(value, name)
        {
        }
    }
}
