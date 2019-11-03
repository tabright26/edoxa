// Filename: ITokenAccount.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface ITokenAccount : IAccount<Token>
    {
        ITransaction Reward(Token amount);

        bool IsDepositAvailable();

        bool HaveSufficientMoney(Token token);
    }
}
