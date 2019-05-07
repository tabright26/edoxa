// Filename: IRandom.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain.Entities.Random
{
    public interface IRandom<out T, in TRange>
    {
        T Next(TRange range);
    }
}