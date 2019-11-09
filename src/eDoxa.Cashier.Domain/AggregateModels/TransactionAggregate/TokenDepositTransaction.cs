// Filename: TokenDepositTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TokenDepositTransaction : Transaction
    {
        public TokenDepositTransaction(TransactionId transactionId, Token currency) : base(
            transactionId,
            currency,
            new TransactionDescription(nameof(TokenDepositTransaction)),
            TransactionType.Deposit,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
