﻿// Filename: IMatchExternalIdAdapter.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IMatchReferencesAdapter
    {
        IEnumerable<MatchReference> MatchReferences { get; }
    }
}
