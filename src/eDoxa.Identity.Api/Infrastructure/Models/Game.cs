// Filename: Game.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Api.Infrastructure.Models
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Game : Enumeration<Game>
    {
        public static readonly Game LeagueOfLegends = new Game(1 << 0, nameof(LeagueOfLegends), true, true);
        public static readonly Game Smite = new Game(1 << 1, nameof(Smite), false, false);
        public static readonly Game StarCraft2 = new Game(1 << 2, nameof(StarCraft2), false, false);
        public static readonly Game Dota2 = new Game(1 << 3, nameof(Dota2), false, true);
        public static readonly Game Csgo = new Game(1 << 4, nameof(Csgo), false, true);
        public static readonly Game RocketLeague = new Game(1 << 5, nameof(RocketLeague), false, false);
        public static readonly Game Overwatch = new Game(1 << 6, nameof(Overwatch), false, true);
        public static readonly Game Fortnite = new Game(1 << 7, nameof(Fortnite), false, false);
        public static readonly Game Pubg = new Game(1 << 8, nameof(Pubg), false, false);

        public Game()
        {
        }

        private Game(
            int value,
            string name,
            bool isSupported,
            bool isDisplayed
        ) : base(value, name)
        {
            IsSupported = isSupported;
            IsDisplayed = isDisplayed;
        }

        public bool IsSupported { get; }

        public bool IsDisplayed { get; }
    }
}
