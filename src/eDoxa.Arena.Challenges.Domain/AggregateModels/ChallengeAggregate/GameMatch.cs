// Filename: GameMatch.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class GameMatch : Match
    {
        public GameMatch(GameScore score, GameReference gameReference, IDateTimeProvider synchronizedAt) : base(new[] {new Stat(new StatName(score.Game), new StatValue(score.ToDecimal()), StatWeighting.None)}, gameReference, synchronizedAt)
        {
        }
    }
}
