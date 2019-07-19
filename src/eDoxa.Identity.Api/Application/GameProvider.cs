// Filename: GameProvider.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Api.Application
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class GameProvider : Enumeration<GameProvider>
    {
        public static readonly GameProvider LeagueOfLegends = new GameProvider(1 << 0, nameof(LeagueOfLegends));
        public static readonly GameProvider Smite = new GameProvider(1 << 1, nameof(Smite));
        public static readonly GameProvider StarCraft2 = new GameProvider(1 << 2, nameof(StarCraft2));
        public static readonly GameProvider Dota2 = new GameProvider(1 << 3, nameof(Dota2));
        public static readonly GameProvider Csgo = new GameProvider(1 << 4, nameof(Csgo));
        public static readonly GameProvider RocketLeague = new GameProvider(1 << 5, nameof(RocketLeague));
        public static readonly GameProvider Overwatch = new GameProvider(1 << 6, nameof(Overwatch));
        public static readonly GameProvider Fortnite = new GameProvider(1 << 7, nameof(Fortnite));
        public static readonly GameProvider Pubg = new GameProvider(1 << 8, nameof(Pubg));

        public GameProvider()
        {
        }

        private GameProvider(int value, string name) : base(value, name)
        {
        }
    }
}
