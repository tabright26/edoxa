// Filename: GameCredential.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate
{
    public sealed class Credential : IAggregateRoot
    {
        public Credential(
            UserId userId,
            Game game,
            PlayerId playerId,
            IDateTimeProvider provider
        ) : this()
        {
            UserId = userId;
            Game = game;
            PlayerId = playerId;
            Timestamp = provider.DateTime;
        }

        private Credential()
        {
        }

        public UserId UserId { get; private set; }

        public Game Game { get; private set; }

        public PlayerId PlayerId { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}
