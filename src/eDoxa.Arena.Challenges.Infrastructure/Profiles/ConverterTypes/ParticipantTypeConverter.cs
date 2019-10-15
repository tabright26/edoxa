// Filename: ParticipantTypeConverter.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ParticipantTypeConverter : ITypeConverter<ParticipantModel, Participant>
    {
        public Participant Convert(ParticipantModel source, Participant destination, ResolutionContext context)
        {
            var participant = new Participant(
                UserId.FromGuid(source.UserId),
                new GameAccountId(source.GameAccountId),
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
