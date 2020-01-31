// Filename: ChallengeExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ChallengeExtensions
    {
        public static ChallengeModel ToModel(this IChallenge challenge)
        {
            var challengeModel = new ChallengeModel
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Game = challenge.Game.Value,
                State = challenge.Timeline.State.Value,
                BestOf = challenge.BestOf,
                Entries = challenge.Entries,
                SynchronizedAt = challenge.SynchronizedAt,
                Timeline = challenge.Timeline.ToModel(),
                ScoringItems = challenge.Scoring.ToModel(),
                Participants = challenge.Participants.Select(participant => participant.ToModel()).ToList()
            };

            foreach (var participant in challengeModel.Participants)
            {
                participant.Challenge = challengeModel;
            }

            return challengeModel;
        }
    }
}
