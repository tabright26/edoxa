// Filename: IScoreboard.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IScoreboard : IReadOnlyDictionary<UserId, Score>
    {
        UserId GetUserId(int index);

        bool IsValidScore(int index);
    }
}
