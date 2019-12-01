// Filename: Game.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

namespace eDoxa.Seedwork.Domain.Misc
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1, nameof(LeagueOfLegends), "League of Legends");
        public static readonly Game Smite = new Game(1 << 1, nameof(Smite), "Smite");
        public static readonly Game StarCraft2 = new Game(1 << 2, nameof(StarCraft2), "StarCraft 2");
        public static readonly Game Dota2 = new Game(1 << 3, nameof(Dota2), "Dota 2");
        public static readonly Game Csgo = new Game(1 << 4, nameof(Csgo), "CSGO");
        public static readonly Game RocketLeague = new Game(1 << 5, nameof(RocketLeague), "Rocket League");
        public static readonly Game Overwatch = new Game(1 << 6, nameof(Overwatch), "Overwatch");
        public static readonly Game Fortnite = new Game(1 << 7, nameof(Fortnite), "Fortnite");
        public static readonly Game Pubg = new Game(1 << 8, nameof(Pubg), "PUBG");

        public Game()
        {
            DisplayName = string.Empty;
        }

        private Game(int value, string name, string displayName) : base(value, name)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }

        public string NormalizedName => Name.ToLowerInvariant();
    }
}
