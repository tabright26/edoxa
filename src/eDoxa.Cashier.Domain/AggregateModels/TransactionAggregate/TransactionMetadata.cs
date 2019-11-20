// Filename: TransactionMetadata.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TransactionMetadata : Dictionary<string, string>
    {
        public TransactionMetadata(IDictionary<string, string> metadata) : base(
            metadata.ToDictionary(x => x.Key.ToUpperInvariant(), x => x.Value.ToUpperInvariant()))
        {
        }

        public TransactionMetadata()
        {
        }
    }
}
