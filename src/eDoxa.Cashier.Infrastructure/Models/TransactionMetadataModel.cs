// Filename: TransactionMetadataModel.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

#nullable disable

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 3.1.
    /// </remarks>
    public class TransactionMetadataModel
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
