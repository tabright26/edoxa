// Filename: TransactionModel.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class TransactionModel
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
    }
}
