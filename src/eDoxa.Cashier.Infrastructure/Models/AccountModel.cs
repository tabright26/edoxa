// Filename: AccountModel.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
