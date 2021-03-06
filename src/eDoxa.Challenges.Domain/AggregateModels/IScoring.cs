﻿// Filename: IScoring.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public interface IScoring : IReadOnlyDictionary<StatName, StatWeighting>
    {
        IEnumerable<Stat> Map(IDictionary<string, double> stats);
    }
}
