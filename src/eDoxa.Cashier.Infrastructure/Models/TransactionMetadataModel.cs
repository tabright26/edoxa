// Filename: MetadataItemModel.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Cashier.Infrastructure.Models
{
    /// <remarks>
    ///     This class is a pure POCO object that represents a database table in EF Core 2.2.
    /// </remarks>
    public class TransactionMetadataModel
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
