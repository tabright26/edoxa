﻿// Filename: ChallengeEndedAt.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeEndedAt : TypeObject<ChallengeEndedAt, DateTime>
    {
        public ChallengeEndedAt(DateTime value) : base(value)
        {
        }
    }
}