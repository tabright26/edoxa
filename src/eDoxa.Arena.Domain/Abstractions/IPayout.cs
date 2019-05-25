// Filename: IPayout.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Domain.Abstractions
{
    public interface IPayout
    {
        Currency Currency { get; }

        IBuckets Buckets { get; }

        IParticipantPrizes GetParticipantPrizes(IScoreboard scoreboard);
    }
}
