﻿// Filename: IPayout.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IPayout
    {
        Currency Currency { get; }

        IBuckets Buckets { get; }

        IPayout ApplyEntryFee(EntryFee entryFee, Currency type);

        IParticipantPrizes GetParticipantPrizes(IScoreboard scoreboard);
    }
}
