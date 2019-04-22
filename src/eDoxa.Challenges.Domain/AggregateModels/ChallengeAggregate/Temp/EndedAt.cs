// Filename: EndedAt.cs
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
    public sealed class EndedAt
    {
        private readonly DateTime _value;

        public EndedAt(StartedAt startedAt, ExtensionPeriod extensionPeriod)
        {
            _value = startedAt + extensionPeriod;
        }

        public static implicit operator DateTime(EndedAt startedAt)
        {
            return startedAt._value;
        }
    }
}