// Filename: Game.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Domain.Enumerations
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends));
        public static readonly Game Csgo = new Game(1 << 1, nameof(Csgo));
        public static readonly Game Fortnite = new Game(1 << 2, nameof(Fortnite));

        private Game(int value, string name) : base(value, name)
        {
        }
    }
}