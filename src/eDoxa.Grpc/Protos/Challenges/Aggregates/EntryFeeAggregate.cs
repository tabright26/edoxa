// Filename: EntryFeeAggregate.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Diagnostics.CodeAnalysis;

namespace eDoxa.Grpc.Protos.Challenges.Aggregates
{
    public partial class EntryFeeAggregate : IComparable<EntryFeeAggregate>
    {
        public int CompareTo([AllowNull] EntryFeeAggregate other)
        {
            if (Currency == other.Currency)
            {
                if (Amount.ToDecimal() < other.Amount.ToDecimal())
                {
                    return 1;
                }

                if (Amount.ToDecimal() > other.Amount.ToDecimal())
                {
                    return -1;
                }

                return 0;
            }

            return Currency.CompareTo(other.Currency);
        }
    }
}
