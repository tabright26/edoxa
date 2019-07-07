﻿// Filename: TransactionModel.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class TransactionModel : PersistentObject
    {
        public DateTime Timestamp { get; set; }

        public decimal Amount { get; set; }

        public int Currency { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public AccountModel Account { get; set; }
    }
}
