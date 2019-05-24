﻿// Filename: TokenPrize.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class TokenPrize : Prize
    {
        public TokenPrize(decimal amount) : base(amount, Currency.Token)
        {
        }
    }
}
