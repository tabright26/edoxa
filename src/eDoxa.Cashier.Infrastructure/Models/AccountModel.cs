// Filename: AccountModel.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }

        public UserModel User { get; set; }

        public ICollection<TransactionModel> Transactions { get; set; }
    }
}
