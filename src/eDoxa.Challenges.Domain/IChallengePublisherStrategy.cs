﻿// Filename: IChallengePublisherStrategy.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain
{
    public interface IChallengePublisherStrategy
    {
        IEnumerable<Challenge> Challenges { get; }
    }
}