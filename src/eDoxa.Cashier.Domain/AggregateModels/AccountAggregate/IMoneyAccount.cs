﻿// Filename: IMoneyAccount.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface IMoneyAccount : IAccount
    {
        DateTime? LastDeposit { get; }

        DateTime? LastWithdraw { get; }

        ITransaction Deposit(Money amount);

        ITransaction Charge(Money amount, TransactionMetadata? metadata = null);

        ITransaction Payout(Money amount, TransactionMetadata? metadata = null);

        ITransaction Promotion(Money amount, TransactionMetadata? metadata = null);

        ITransaction Withdraw(Money amount);

        bool HaveSufficientMoney(Money money);

        bool IsDepositAvailable();

        bool IsWithdrawAvailable();
    }
}
