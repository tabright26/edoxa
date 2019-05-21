// Filename: ChallengeName.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeName : TypeObject<ChallengeName, string>
    {
        public ChallengeName(string name) : base(name)
        {
            if (string.IsNullOrWhiteSpace(name) || !name.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '(' || c == ')'))
            {
                throw new ArgumentException(nameof(name));
            }
        }
    }
}
