// Filename: ParticipantModelExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ParticipantModelExtensions
    {
        public static Participant ToEntity(this ParticipantModel model)
        {
            var participant = new Participant(
                ParticipantId.FromGuid(model.Id),
                UserId.FromGuid(model.UserId),
                PlayerId.Parse(model.PlayerId),
                new DateTimeProvider(model.RegisteredAt));

            if (model.SynchronizedAt.HasValue && model.Matches != null)
            {
                participant.Snapshot(model.Matches.Select(match => match.ToEntity()), new DateTimeProvider(model.SynchronizedAt.Value));
            }

            participant.ClearDomainEvents();

            return participant;
        }
    }
}
