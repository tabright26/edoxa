// Filename: ParticipantTypeConverter.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ParticipantTypeConverter : ITypeConverter<ParticipantModel, Participant>
    {
        public Participant Convert(ParticipantModel source, Participant destination, ResolutionContext context)
        {
            var participant = new Participant(
                UserId.FromGuid(source.UserId),
                PlayerId.Parse(source.PlayerId),
                new DateTimeProvider(source.RegisteredAt)
            );

            participant.SetEntityId(ParticipantId.FromGuid(source.Id));

            var matches = context.Mapper.Map<ICollection<IMatch>>(source.Matches);

            foreach (var match in matches)
            {
                participant.Snapshot(match);
            }

            return participant;
        }
    }
}
