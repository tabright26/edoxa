﻿// Filename: AccountModel.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class AccountModel : PersistentObject
    {
        public Guid UserId { get; set; }

        public ICollection<TransactionModel> Transactions { get; set; }
    }
}
