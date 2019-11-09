// Filename: ITokenAccount.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface ITokenAccount : IAccount<Token>
    {
        ITransaction Reward(TransactionId transactionId, Token amount);

        bool IsDepositAvailable();

        bool HaveSufficientMoney(Token token);
    }
}
