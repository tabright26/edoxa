// Filename: IBucketFactors.cs
// Date Created: 2019-05-23
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
    public interface IBucketFactors : IReadOnlyList<BucketFactor>
    {
        IPayout CreatePayout(Prize prize);
    }
}
