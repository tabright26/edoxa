// Filename: UserModel.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Infrastructure.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string ConnectAccountId { get; set; }

        public string CustomerId { get; set; }

        public string BankAccountId { get; set; }

        public AccountModel Account { get; set; }
    }
}
