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
using eDoxa.Seedwork.Domain.TypeConverters;

namespace eDoxa.Seedwork.Domain.Common.Enumerations
{
    [TypeConverter(typeof(EnumerationTypeConverter<Game>))]
    public sealed class Game : Enumeration
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends));

        private Game(int value, string name) : base(value, name)
        {
        }
    }
}