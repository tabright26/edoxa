// Filename: ClosedAt.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Temp
{
    public sealed class ClosedAt
    {
        private readonly DateTime _value;

        public ClosedAt()
        {
            _value = DateTime.UtcNow;
        }

        internal ClosedAt(DateTime closedAt)
        {
            _value = closedAt;
        }

        public static implicit operator DateTime(ClosedAt closedAt)
        {
            return closedAt._value;
        }
    }
}