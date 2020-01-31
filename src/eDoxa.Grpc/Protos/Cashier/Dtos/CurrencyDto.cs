// Filename: CurrencyDto.cs
// Date Created: 2020-01-29
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Diagnostics.CodeAnalysis;

namespace eDoxa.Grpc.Protos.Cashier.Dtos
{
    public partial class CurrencyDto : IComparable<CurrencyDto>
    {
        public int CompareTo([AllowNull] CurrencyDto other)
        {
            if (Type == other.Type)
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

            return Type.CompareTo(other.Type);
        }
    }
}
