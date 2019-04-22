// Filename: CreateAt.cs
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
    public sealed class CreateAt
    {
        private readonly DateTime _value;

        public CreateAt()
        {
            _value = DateTime.UtcNow;
        }

        internal CreateAt(DateTime createAt)
        {
            _value = createAt;
        }

        public static implicit operator DateTime(CreateAt createAt)
        {
            return createAt._value;
        }
    }
}