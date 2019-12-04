// Filename: TokenRewardTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TokenRewardTransaction : Transaction
    {
        public TokenRewardTransaction(Token currency) : base(
            currency,
            new TransactionDescription(nameof(TokenRewardTransaction)),
            TransactionType.Reward,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
