// Filename: TransactionMetadata.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TransactionMetadata : Dictionary<string, string>
    {
        public TransactionMetadata(IDictionary<string, string> metadata) : base(metadata)
        {
        }

        public TransactionMetadata()
        {
        }
    }
}
