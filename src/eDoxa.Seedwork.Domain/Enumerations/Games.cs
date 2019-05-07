// Filename: Games.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Enumerations
{
    public sealed class Games : Enumeration<Games>
    {
        public static readonly Games LeagueOfLegends = new Games(1 << 0, nameof(LeagueOfLegends));
        public static readonly Games CSGO = new Games(1 << 1, nameof(CSGO));
        public static readonly Games Fortnite = new Games(1 << 2, nameof(Fortnite));

        private Games(int value, string name) : base(value, name)
        {
        }
    }
}