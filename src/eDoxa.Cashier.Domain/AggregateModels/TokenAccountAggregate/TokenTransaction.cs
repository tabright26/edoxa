﻿// Filename: TokenTransaction.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenTransaction : Transaction<Token>, ITokenTransaction
    {
        public TokenTransaction(Token amount) : base(amount)
        {
        }

        protected TokenTransaction(Token amount, ActivityId activityId) : base(amount, activityId)
        {
        }
    }
}