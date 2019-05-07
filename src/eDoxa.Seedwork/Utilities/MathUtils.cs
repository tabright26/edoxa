// Filename: Optimization.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Utilities
{
    public static class MathUtils
    {
        public static int RoundMultiplier(decimal value, int multiplier)
        {
            return (int)Math.Round(value / multiplier);
        }
    }
}