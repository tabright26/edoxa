// Filename: IPayout.cs
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
        IBuckets Buckets { get; }

        IPayout ApplyFactor(PayoutFactor factor);

        IParticipantPrizes GetParticipantPrizes(IScoreboard scoreboard);
    }
}
