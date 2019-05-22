// Filename: IBuckets.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IBuckets : IReadOnlyList<Bucket>
    {
        Prize GetPrize(int index);

        IBuckets ApplyFactor(EntryFeeType factor);
    }
}
