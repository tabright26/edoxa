// Filename: ITokenAccount.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface ITokenAccount : IAccount
    {
        DateTime? LastDeposit { get; }

        ITransaction Deposit(Token amount);

        ITransaction Charge(Token amount, TransactionMetadata? metadata = null);

        ITransaction Payout(Token amount, TransactionMetadata? metadata = null);

        ITransaction Reward(Token amount, TransactionMetadata? metadata = null);

        bool IsDepositAvailable();

        bool HaveSufficientMoney(Token token);
    }
}
