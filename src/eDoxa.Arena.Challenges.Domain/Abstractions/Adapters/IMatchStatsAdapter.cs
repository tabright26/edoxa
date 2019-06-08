// Filename: IMatchStatsAdapter.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Adapters
{
    public interface IMatchStatsAdapter
    {
        IMatchStats MatchStats { get; }
    }
}
