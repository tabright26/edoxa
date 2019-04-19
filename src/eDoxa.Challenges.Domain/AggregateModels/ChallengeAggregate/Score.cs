// Filename: Score.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;
using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class Score : ValueObject
    {
        private const int RoundDecimals = 2;

        private readonly decimal _value;

        private Score(decimal value)
        {
            _value = value;
        }

        public static Score FromDouble(double score)
        {
            var value = Convert.ToDecimal(score);

            return new Score(value);
        }

        public decimal ToDecimal()
        {
            return _value;
        }

        [CanBeNull]
        internal static Score FromParticipant(Participant participant)
        {
            var bestOf = participant.Challenge.Settings.BestOf;

            if (participant.Matches.Count < bestOf.ToInt32())
            {
                return null;
            }

            var averageScore = participant.Matches.OrderBy(match => match.TotalScore).Take(bestOf.ToInt32()).Average(match => match.TotalScore.ToDecimal());

            return RoundScore(averageScore);
        }

        internal static Score FromMatch(Match match)
        {
            var totalScore = match.Stats.Sum(stat => stat.Score.ToDecimal());

            return RoundScore(totalScore);
        }

        internal static Score FromStat(Stat stat)
        {
            var value = Convert.ToDecimal(stat.Value);

            var weighting = Convert.ToDecimal(stat.Weighting);

            var score = value * weighting;

            return RoundScore(score);
        }

        private static Score RoundScore(decimal score)
        {
            var roundScore = Math.Round(score, RoundDecimals);

            return new Score(roundScore);
        }
    }

    public sealed partial class Score : IComparable, IComparable<Score>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as Score);
        }

        public int CompareTo([CanBeNull] Score other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}