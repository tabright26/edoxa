// Filename: GameTournamentInfo.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Arena.Games.Domain.AggregateModels
{
    public sealed class GameTournamentInfo
    {
        public GameTournamentInfo(bool displayed, bool enabled)
        {
            Displayed = displayed;
            Enabled = enabled;
        }

        public bool Displayed { get; }

        public bool Enabled { get; }
    }
}
