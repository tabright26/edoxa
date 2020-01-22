// Filename: AccountModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class AccountModel
    {
        public Guid Id { get; set; }

        public ICollection<TransactionModel> Transactions { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
