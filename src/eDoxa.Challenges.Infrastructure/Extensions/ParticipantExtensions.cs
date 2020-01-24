// Filename: ParticipantExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ParticipantExtensions
    {
        public static ParticipantModel ToModel(this Participant participant)
        {
            var participantModel = new ParticipantModel
            {
                Id = participant.Id,
                RegisteredAt = participant.RegisteredAt,
                SynchronizedAt = participant.SynchronizedAt,
                PlayerId = participant.PlayerId,
                UserId = participant.UserId,
                Matches = participant.Matches.Select(match => match.ToModel()).ToList()
            };

            foreach (var match in participantModel.Matches)
            {
                match.Participant = participantModel;
            }

            return participantModel;
        }
    }
}
