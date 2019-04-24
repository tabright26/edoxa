// Filename: MoneyAccount.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class MoneyAccount : IAccount<Money>
    {
        private Money _balance;

        private Money _pending;

        public MoneyAccount()
        {
            _balance = Money.Zero;
            _pending = Money.Zero;
        }

        public Money Balance => _balance;

        public Money Pending => _pending;

        public void AddBalance(Money amount)
        {
            _balance = new Money(_balance + amount);
        }

        public void SubtractBalance(Money amount)
        {
            _balance = new Money(_balance - amount);
        }

        public void AddPending(Money amount)
        {
            _pending = new Money(_pending + amount);
        }

        public void SubtractPending(Money amount)
        {
            _pending = new Money(_pending - amount);
        }
    }
}