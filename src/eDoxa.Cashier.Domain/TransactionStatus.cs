﻿// Filename: TransactionStatus.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.TypeConverters;

namespace eDoxa.Cashier.Domain
{
    [TypeConverter(typeof(EnumerationTypeConverter<TransactionStatus>))]
    public sealed class TransactionStatus : Enumeration
    {
        public static readonly TransactionStatus Pending = new TransactionStatus(1 << 0, nameof(Pending));
        public static readonly TransactionStatus Completed = new TransactionStatus(1 << 1, nameof(Completed));
        public static readonly TransactionStatus Failed = new TransactionStatus(1 << 2, nameof(Failed));

        private TransactionStatus(int value, string name) : base(value, name)
        {
        }
    }
}
