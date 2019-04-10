// Filename: Game.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.Domain.Common.Enums
{
    [Flags]
    public enum Game
    {
        None = 0,
        LeagueOfLegends = 1 << 0,
        CSGO = 1 << 1,
        Fortnite = 1 << 2,
        All = ~None
    }
}