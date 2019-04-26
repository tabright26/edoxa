// Filename: MoneyPendingTransaction.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class MoneyPendingTransaction : MoneyTransaction
    {
        public MoneyPendingTransaction(Money amount, ActivityId activityId) : base(amount, activityId)
        {
        }
    }
}