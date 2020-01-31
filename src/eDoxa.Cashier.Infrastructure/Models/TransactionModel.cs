// Filename: TransactionModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.SqlServer;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class TransactionModel : IEntityModel
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal Amount { get; set; }

        public int Currency { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public AccountModel Account { get; set; }

        public ICollection<TransactionMetadataModel> Metadata { get; set; }

        public ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
