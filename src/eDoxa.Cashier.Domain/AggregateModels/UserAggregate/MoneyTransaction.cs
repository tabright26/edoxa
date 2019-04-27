// Filename: MoneyTransaction.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class MoneyTransaction : Transaction<Money>, IMoneyTransaction
    {
        public MoneyTransaction(Money amount) : base(amount)
        {
        }

        protected MoneyTransaction(Money amount, ActivityId activityId) : base(amount, activityId.ToString())
        {
        }

        public Option<MoneyTransaction> TryPayoff(Money amount)
        {
            this.Complete();

            return -Amount < amount ? new Option<MoneyTransaction>(new MoneyTransaction(amount)) : new Option<MoneyTransaction>();
        }
    }
}