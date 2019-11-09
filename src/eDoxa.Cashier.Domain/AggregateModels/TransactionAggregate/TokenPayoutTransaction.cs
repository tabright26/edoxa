// Filename: TokenPayoutTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TokenPayoutTransaction : Transaction
    {
        public TokenPayoutTransaction(TransactionId transactionId, Token currency) : base(
            transactionId,
            currency,
            new TransactionDescription(nameof(TokenPayoutTransaction)),
            TransactionType.Payout,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
