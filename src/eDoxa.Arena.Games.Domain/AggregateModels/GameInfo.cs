// Filename: GameInfo.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.AggregateModels
{
    public sealed class GameInfo
    {
        private GameInfo(
            GameCredential credential,
            bool enabled,
            bool displayed,
            GameChallengeInfo challengeInfo,
            GameTournamentInfo tournamentInfo
        ) : this(
            credential.Game,
            enabled,
            true,
            displayed,
            challengeInfo,
            tournamentInfo)
        {
        }

        public GameInfo(
            Game game,
            bool enabled,
            bool displayed,
            GameChallengeInfo challengeInfo,
            GameTournamentInfo tournamentInfo
        ) : this(
            game.Name,
            game.DisplayName,
            displayed,
            false,
            enabled,
            challengeInfo,
            tournamentInfo)
        {
        }

        private GameInfo(
            Game game,
            bool enabled,
            bool linked,
            bool displayed,
            GameChallengeInfo challengeInfo,
            GameTournamentInfo tournamentInfo
        ) : this(
            game.Name,
            game.DisplayName,
            displayed,
            linked,
            enabled,
            challengeInfo,
            tournamentInfo)
        {
        }

        private GameInfo(
            string name,
            string displayName,
            bool displayed,
            bool linked,
            bool enabled,
            GameChallengeInfo challengeInfo,
            GameTournamentInfo tournamentInfo
        )
        {
            Name = name.ToLowerInvariant();
            DisplayName = displayName;
            Enabled = enabled;
            Linked = linked;
            Displayed = displayed;
            ChallengeInfo = challengeInfo;
            TournamentInfo = tournamentInfo;
        }

        public string Name { get; }

        public string DisplayName { get; }

        public bool Displayed { get; }

        public bool Linked { get; }

        public bool Enabled { get; }

        public GameChallengeInfo ChallengeInfo { get; }

        public GameTournamentInfo TournamentInfo { get; }

        public GameInfo WithCredential(GameCredential? credential)
        {
            return credential == null
                ? this
                : new GameInfo(
                    credential,
                    Enabled,
                    Displayed,
                    ChallengeInfo,
                    TournamentInfo);
        }
    }
}
