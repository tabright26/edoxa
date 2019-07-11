// Filename: Game.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels
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
