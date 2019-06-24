// Filename: Game.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Common.Enumerations
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends));

        public Game()
        {
        }

        private Game(int value, string name) : base(value, name)
        {
        }
    }
}
