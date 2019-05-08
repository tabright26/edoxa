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
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends));
        public static readonly Game CSGO = new Game(1 << 1, nameof(CSGO));
        public static readonly Game Fortnite = new Game(1 << 2, nameof(Fortnite));

        private Game(int value, string displayName) : base(value, displayName)
        {
        }

        public Game()
        {
        }
    }
}