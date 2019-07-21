// Filename: Game.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Api.Application
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends));
        public static readonly Game Smite = new Game(1 << 1, nameof(Smite));
        public static readonly Game StarCraft2 = new Game(1 << 2, nameof(StarCraft2));
        public static readonly Game Dota2 = new Game(1 << 3, nameof(Dota2));
        public static readonly Game Csgo = new Game(1 << 4, nameof(Csgo));
        public static readonly Game RocketLeague = new Game(1 << 5, nameof(RocketLeague));
        public static readonly Game Overwatch = new Game(1 << 6, nameof(Overwatch));
        public static readonly Game Fortnite = new Game(1 << 7, nameof(Fortnite));
        public static readonly Game Pubg = new Game(1 << 8, nameof(Pubg));

        public Game()
        {
        }

        private Game(int value, string name) : base(value, name)
        {
        }
    }
}
