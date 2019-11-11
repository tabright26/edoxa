// Filename: TokenChargeTransaction.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TokenChargeTransaction : Transaction
    {
        public TokenChargeTransaction(Token amount, TransactionMetadata? metadata = null) : base(
            -amount,
            new TransactionDescription(nameof(TokenChargeTransaction)),
            TransactionType.Charge,
            new UtcNowDateTimeProvider(),
            metadata)
        {
        }
    }
}
